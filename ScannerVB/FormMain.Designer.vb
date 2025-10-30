<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        BtnQuitter = New Button()
        BtnAnalyse = New Button()
        BtnExporter = New Button()
        CMF = New OpenFileDialog()
        BtnRecherche = New Button()
        SuspendLayout()
        ' 
        ' BtnQuitter
        ' 
        BtnQuitter.Location = New Point(363, 29)
        BtnQuitter.Margin = New Padding(3, 2, 3, 2)
        BtnQuitter.Name = "BtnQuitter"
        BtnQuitter.Size = New Size(100, 36)
        BtnQuitter.TabIndex = 15
        BtnQuitter.Text = "Quitter"
        BtnQuitter.UseVisualStyleBackColor = True
        ' 
        ' BtnAnalyse
        ' 
        BtnAnalyse.Location = New Point(12, 27)
        BtnAnalyse.Margin = New Padding(3, 2, 3, 2)
        BtnAnalyse.Name = "BtnAnalyse"
        BtnAnalyse.Size = New Size(98, 40)
        BtnAnalyse.TabIndex = 13
        BtnAnalyse.Text = "Analyser"
        BtnAnalyse.UseVisualStyleBackColor = True
        ' 
        ' BtnExporter
        ' 
        BtnExporter.Location = New Point(116, 29)
        BtnExporter.Margin = New Padding(3, 2, 3, 2)
        BtnExporter.Name = "BtnExporter"
        BtnExporter.Size = New Size(98, 40)
        BtnExporter.TabIndex = 14
        BtnExporter.Text = "Exporter CSV"
        BtnExporter.UseVisualStyleBackColor = True
        ' 
        ' BtnRecherche
        ' 
        BtnRecherche.Location = New Point(244, 29)
        BtnRecherche.Margin = New Padding(3, 2, 3, 2)
        BtnRecherche.Name = "BtnRecherche"
        BtnRecherche.Size = New Size(98, 40)
        BtnRecherche.TabIndex = 16
        BtnRecherche.Text = "Rechercher"
        BtnRecherche.UseVisualStyleBackColor = True
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(475, 85)
        Controls.Add(BtnRecherche)
        Controls.Add(BtnQuitter)
        Controls.Add(BtnAnalyse)
        Controls.Add(BtnExporter)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "FormMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Scanner de Projets Visual Basic 6"
        ResumeLayout(False)
    End Sub
    Friend WithEvents BtnQuitter As Button
    Friend WithEvents BtnAnalyse As Button
    Friend WithEvents BtnExporter As Button
    Friend WithEvents CMF As OpenFileDialog
    Friend WithEvents BtnRecherche As Button
End Class
