<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InterfaceWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.StatusBar = New System.Windows.Forms.StatusStrip()
        Me.AnglesLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ZoonLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.HelpMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.FileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.StatusBar.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusBar
        '
        Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnglesLabel, Me.ZoonLabel})
        Me.StatusBar.Location = New System.Drawing.Point(0, 428)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(800, 22)
        Me.StatusBar.TabIndex = 0
        Me.StatusBar.Text = "StatusStrip1"
        '
        'AnglesLabel
        '
        Me.AnglesLabel.BackColor = System.Drawing.SystemColors.Control
        Me.AnglesLabel.Name = "AnglesLabel"
        Me.AnglesLabel.Size = New System.Drawing.Size(0, 17)
        '
        'ZoonLabel
        '
        Me.ZoonLabel.BackColor = System.Drawing.SystemColors.Control
        Me.ZoonLabel.Name = "ZoonLabel"
        Me.ZoonLabel.Size = New System.Drawing.Size(0, 17)
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMainMenu, Me.HelpMainMenu})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(800, 24)
        Me.MenuBar.TabIndex = 1
        Me.MenuBar.Text = "MenuStrip1"
        '
        'HelpMainMenu
        '
        Me.HelpMainMenu.Name = "HelpMainMenu"
        Me.HelpMainMenu.Size = New System.Drawing.Size(44, 20)
        Me.HelpMainMenu.Text = "&Help"
        '
        'FileMainMenu
        '
        Me.FileMainMenu.Name = "FileMainMenu"
        Me.FileMainMenu.Size = New System.Drawing.Size(37, 20)
        Me.FileMainMenu.Text = "&File"
        '
        'FileDialog
        '
        '
        'InterfaceWindow
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.MenuBar)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuBar
        Me.Name = "InterfaceWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar.ResumeLayout(False)
        Me.StatusBar.PerformLayout()
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents AnglesLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ZoonLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents HelpMainMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileMainMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents FileDialog As System.Windows.Forms.OpenFileDialog
End Class
