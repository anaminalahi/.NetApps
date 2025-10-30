'CLASSE DE BASE POUR LES VUE-MODELE DE LA METHODE MVVM

Public MustInherit Class ViewModelBase
    Implements System.ComponentModel.INotifyPropertyChanged

    'Evénement de l'interface à déclarer
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    'Procédure à appeller pour alerter le client d'une modification de valeur d'une propriété
    Public Sub OnPropertyChanged(ByVal NomPropriete As String)
        If String.IsNullOrEmpty(NomPropriete) = False Then
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(NomPropriete))
        End If
    End Sub


End Class
