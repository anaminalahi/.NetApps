<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNouvelleValeur
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
        Me.BtnEnregistrer = New System.Windows.Forms.Button()
        Me.BtnAnnuler = New System.Windows.Forms.Button()
        Me.txtCle = New System.Windows.Forms.TextBox()
        Me.txtValeur = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnEnregistrer
        '
        Me.BtnEnregistrer.Location = New System.Drawing.Point(15, 89)
        Me.BtnEnregistrer.Name = "BtnEnregistrer"
        Me.BtnEnregistrer.Size = New System.Drawing.Size(93, 40)
        Me.BtnEnregistrer.TabIndex = 0
        Me.BtnEnregistrer.Text = "Enregistrer"
        Me.BtnEnregistrer.UseVisualStyleBackColor = True
        '
        'BtnAnnuler
        '
        Me.BtnAnnuler.Location = New System.Drawing.Point(359, 89)
        Me.BtnAnnuler.Name = "BtnAnnuler"
        Me.BtnAnnuler.Size = New System.Drawing.Size(93, 40)
        Me.BtnAnnuler.TabIndex = 1
        Me.BtnAnnuler.Text = "Annuler"
        Me.BtnAnnuler.UseVisualStyleBackColor = True
        '
        'txtCle
        '
        Me.txtCle.Location = New System.Drawing.Point(74, 23)
        Me.txtCle.Name = "txtCle"
        Me.txtCle.Size = New System.Drawing.Size(141, 20)
        Me.txtCle.TabIndex = 2
        '
        'txtValeur
        '
        Me.txtValeur.Location = New System.Drawing.Point(74, 49)
        Me.txtValeur.Name = "txtValeur"
        Me.txtValeur.Size = New System.Drawing.Size(378, 20)
        Me.txtValeur.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Clé"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Valeur"
        '
        'FormNouvelleValeur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightBlue
        Me.ClientSize = New System.Drawing.Size(464, 131)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtValeur)
        Me.Controls.Add(Me.txtCle)
        Me.Controls.Add(Me.BtnAnnuler)
        Me.Controls.Add(Me.BtnEnregistrer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FormNouvelleValeur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Saisie d'une nouvelle valeur"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnEnregistrer As Forms.Button
    Friend WithEvents BtnAnnuler As Forms.Button
    Friend WithEvents txtCle As Forms.TextBox
    Friend WithEvents txtValeur As Forms.TextBox
    Friend WithEvents Label1 As Forms.Label
    Friend WithEvents Label2 As Forms.Label
End Class
