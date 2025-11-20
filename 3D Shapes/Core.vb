'This modules's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Environment
Imports System.IO
Imports System.Linq
Imports System.Math
Imports System.Windows.Forms

'This module contains this program's core procedures.
Public Module CoreModule
   Public Const DEFAULT_ZOOM As Double = 200.0    'Defines the default zoom.
   Private Const COMMENT_MARKER As Char = "#"c     'Defines a comment marker in shape files.
   Private Const DELIMITER As Char = " "c          'Defines a delimiter in shape files.
   Private Const LINE_MARKER As Char = "l"c        'Defines a line marker in shape files.
   Private Const VERTEX_MARKER As Char = "v"c      'Defines a vertex marker in shape files.

   Private ReadOnly MAXIMUM_ANGLE As Double = PI * 2   'Defines the maximum angle allowed.

   'This enumeration lists the axes used.
   Public Enum AxesE As Integer
      x   'Defines the x axis.
      y   'Defines the y axis.
      z   'Defines the z axis.
   End Enum

   'This structure defines a line made up of two vertices.
   Public Structure LineStr
      Public Start As Integer   'Defines a line's starting point's index.
      Public [End] As Integer   'Defines a line's ending point's index.

      Public Sub New(NewStart As Integer, NewEnd As Integer)
         Start = NewStart
         [End] = NewEnd
      End Sub
   End Structure

   'This structure defines a 3-dimensional shape.
   Public Structure ShapeStr
      Public Lines As List(Of LineStr)          'Defines a shape's lines.
      Public Vertices As List(Of Vertex3DStr)   'Defines a shape's vertices.
   End Structure

   'This structure defines a vertex.
   Public Structure Vertex3DStr
      Public X As Double   'Defines an x coordinate.
      Public Y As Double   'Defines a y coordinate.
      Public Z As Double   'Defines a z coordinate.

      'This procedure creates a vertex with the specified coordinates.
      Public Sub New(NewX As Double, NewY As Double, NewZ As Double)
         Me.X = NewX
         Me.Y = NewY
         Me.Z = NewZ
      End Sub
   End Structure

   Public AngleX As New Double                                                                                'Contains the angle along the x-axis.
   Public AngleY As New Double                                                                                'Contains the angle along the y-axis.
   Public AngleZ As New Double                                                                                'Contains the angle along the z-axis.
   Public CameraZ As New Integer                                                                              'Contains the camera's distance.
   Public Shape As New ShapeStr With {.Lines = New List(Of LineStr), .Vertices = New List(Of Vertex3DStr)}    'Contains the shape.
   Public Zoom As New Double                                                                                  'Contains the zoom factor.

   'This procedure increases/decreases the specified angle and returns the result.
   Public Function AdjustAngle(Angle As Double, Increase As Boolean, Optional Change As Double = 0.05) As Double
      Try
         If Increase Then
            If Angle + Change > MAXIMUM_ANGLE Then
               Angle = (Angle + Change) - MAXIMUM_ANGLE
            Else
               Angle += Change
            End If
         Else
            If Angle - Change < 0 Then
               Angle = MAXIMUM_ANGLE + (Angle - Change)
            Else
               Angle -= Change
            End If
         End If

         Return Angle
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure displays any errors that occur.
   Public Sub DisplayError(ExceptionO As exception)
      Dim Message As String = ExceptionO.Message

      Try
         MessageBox.Show(Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure initializes the display parameters.
   Public Sub InitializeDisplayParameters()
      Try
         AngleX = 0
         AngleY = 0
         AngleZ = 0
         Zoom = DEFAULT_ZOOM
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure returns a shape's data loaded from the specified file.
   Public Function LoadShape(FilePath As String) As ShapeStr
      Try
         Dim Items() As String = {}
         Dim Shape As New ShapeStr

         Shape.Vertices = New List(Of Vertex3DStr)()
         Shape.Lines = New List(Of LineStr)()

         For Each Line As String In File.ReadLines(FilePath)
            Line = Line.Trim()

            If Not (String.IsNullOrWhiteSpace(Line) OrElse Line.StartsWith(COMMENT_MARKER)) Then
               Items = Line.Split(DELIMITER)

               Select Case Items.First.Trim().ToLower()
                  Case LINE_MARKER
                     If Items.Length >= 3 Then
                        Shape.Lines.Add(New LineStr(NewStart:=Integer.Parse(Items(1)), NewEnd:=Integer.Parse(Items(2))))
                     End If
                  Case VERTEX_MARKER
                     If Items.Length >= 4 Then
                        Shape.Vertices.Add(New Vertex3DStr(NewX:=Double.Parse(Items(1)), NewY:=Double.Parse(Items(2)), NewZ:=Double.Parse(Items(3))))
                     End If
               End Select
            End If
         Next Line

         CameraZ = CInt(GetMaximumCoordinate(Shape.Vertices) * 2)

         InitializeDisplayParameters()

         Return Shape
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure returns the coordinate furthest from the shape's center.
   Private Function GetMaximumCoordinate(Vertices As List(Of Vertex3DStr)) As Double
      Try
         Dim Maximum As Double = Double.MinValue
         Dim MaximumCoordinate As New Double

         For Each Vertex As Vertex3DStr In Vertices
            Maximum = {Abs(Vertex.X), Abs(Vertex.Y), Abs(Vertex.Z)}.Max()
            If Maximum >= MaximumCoordinate Then MaximumCoordinate = Maximum
         Next Vertex

         Return MaximumCoordinate
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure returns information about this program.
   Public Function ProgramInformation() As String
      Try
         With My.Application.Info
            Return $"{ .Title} v{ .Version} - by: { .CompanyName}"
         End With
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure rotates a vertex along the specified axis and returns the result.
   Public Function Rotate(Vertex As Vertex3DStr, Angle As Double, Axis As AxesE) As Vertex3DStr
      Try
         Dim RotatedVertex As Vertex3DStr

         Select Case Axis
            Case AxesE.x
               RotatedVertex = New Vertex3DStr(Vertex.X, (Vertex.Y * Cos(Angle)) - Vertex.Z * Sin(Angle), (Vertex.Y * Sin(Angle)) + (Vertex.Z * Cos(Angle)))
            Case AxesE.y
               RotatedVertex = New Vertex3DStr((Vertex.X * Cos(Angle)) + Vertex.Z * Sin(Angle), Vertex.Y, (-Vertex.X * Sin(Angle)) + (Vertex.Z * Cos(Angle)))
            Case AxesE.z
               RotatedVertex = New Vertex3DStr((Vertex.X * Cos(Angle)) - (Vertex.Y * Sin(Angle)), (Vertex.X * Sin(Angle)) + (Vertex.Y * Cos(Angle)), Vertex.Z)
         End Select

         Return RotatedVertex
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try

      Return Nothing
   End Function
End Module
