'This class's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Environment
Imports System.Linq
Imports System.Windows.Forms

'This class contains this program's main interface.
Public Class InterfaceWindow

   'This procedure initializes this program.
   Public Sub New()
      Try
         InitializeComponent()

         Me.Width = CInt(My.Computer.Screen.Bounds.Width / 2)
         Me.Height = CInt(My.Computer.Screen.Bounds.Height / 2)

         Me.Text = ProgramInformation()

         InitializeDisplayParameters()

         If GetCommandLineArgs().Count > 1 Then
            Shape = LoadShape(GetCommandLineArgs().Last())
         End If

         ToolTip.SetToolTip(Me, "Drag a shape file here to load it.")
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure displays the open file dialog.
   Private Sub FileMainMenu_Click(sender As Object, e As EventArgs) Handles FileMainMenu.Click
      Try
         With FileDialog
            If .ShowDialog() = DialogResult.OK Then
               Shape = LoadShape(.FileName)
               Me.Invalidate()
            End If
         End With
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure displays this program's help.
   Private Sub HelpMainMenu_Click(sender As Object, e As EventArgs) Handles HelpMainMenu.Click
      Try
         MessageBox.Show(My.Resources.Help.ToString(), My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure handles files being dropped into this window.
   Private Sub InterfaceWindow_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
      Try
         With CType(e.Data.GetData(DataFormats.FileDrop), String())
            If .Count > 0 Then
               Shape = LoadShape(.Last())
               Me.Invalidate()
            End If
         End With
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure handles files being dragged into this window.
   Private Sub InterfaceWindow_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
      Try
         If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
         End If
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure gives the command to refresh this window after resizing.
   Private Sub InterfaceWindow_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
      Try
         Me.Invalidate()
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure handles the user's key strokes.
   Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
      Try
         Select Case e.KeyCode
            Case Keys.Add, Keys.Oemplus
               Zoom += 10.0
            Case Keys.Down
               AngleX = AdjustAngle(AngleX, Increase:=True)
            Case Keys.Left
               AngleY = AdjustAngle(AngleY, Increase:=False)
            Case Keys.OemMinus, Keys.Subtract
               Zoom -= 10.0F
            Case Keys.PageDown
               AngleZ = AdjustAngle(AngleZ, Increase:=False)
            Case Keys.PageUp
               AngleZ = AdjustAngle(AngleZ, Increase:=True)
            Case Keys.R
               InitializeDisplayParameters()
            Case Keys.Right
               AngleY = AdjustAngle(AngleY, Increase:=True)
            Case Keys.Up
               AngleX = AdjustAngle(AngleX, Increase:=False)
         End Select

         Me.Invalidate()
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure draws the current shape.
   Protected Overrides Sub OnPaint(e As PaintEventArgs)
      Try
         Dim DisplayedShape As New List(Of PointF)
         Dim Factor As New Double
         Dim RotatedVertex As New Vertex3DStr

         For Each Vertex As Vertex3DStr In Shape.Vertices
            RotatedVertex = Rotate(Vertex, AngleX, AxesE.x)
            RotatedVertex = Rotate(RotatedVertex, AngleY, AxesE.y)
            RotatedVertex = Rotate(RotatedVertex, AngleZ, AxesE.z)
            Factor = Zoom / (RotatedVertex.Z + CameraZ)
            DisplayedShape.Add(New PointF(CSng((RotatedVertex.X * Factor) + (Me.ClientSize.Width / 2)), CSng((-RotatedVertex.Y * Factor) + (Me.ClientSize.Height / 2))))
         Next Vertex

         Shape.Lines.ForEach(Sub(Line) e.Graphics.DrawLine(Pens.White, DisplayedShape(Line.Start), DisplayedShape(Line.End)))

         UpdateStatusBar()
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub

   'This procedure updates the status bar.
   Private Sub UpdateStatusBar()
      Try
         AnglesLabel.Text = $"Angles: {AngleX:0.00}, {AngleY:0.00}, {AngleZ:0.00}"
         ZoonLabel.Text = $"Zoom: {Zoom}"
      Catch ExceptionO As Exception
         DisplayError(ExceptionO)
      End Try
   End Sub
End Class
