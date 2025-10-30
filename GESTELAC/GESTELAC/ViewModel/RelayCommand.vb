
'IMPLEMENTATION DE L'INTERFACE ICOMMAND POUR LES COMMANDES DES VUE-MODELE DE LA METHODE MVVM


Public Class RelayCommand
    Implements System.Windows.Input.ICommand

    Private ReadOnly _execute As Action
    Private ReadOnly _executeP As Action(Of Object)
    Private ReadOnly _canExecute As Func(Of Boolean)

    Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
        If _execute IsNot Nothing Then
            _execute()
        ElseIf _executeP IsNot Nothing Then
            _executeP(parameter)
        End If
    End Sub

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
        If _canExecute Is Nothing Then Return True
        Return _canExecute()
    End Function

    'Evenement customisé pour abonner la méthode _canExecute à la notofication du changement (sinon les commandes client ne seront pas alertés !)
    Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)
            If _canExecute IsNot Nothing Then
                AddHandler System.Windows.Input.CommandManager.RequerySuggested, value
            End If
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            If _canExecute IsNot Nothing Then
                RemoveHandler System.Windows.Input.CommandManager.RequerySuggested, value
            End If
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            If _canExecute IsNot Nothing Then
                System.Windows.Input.CommandManager.InvalidateRequerySuggested()
            End If
        End RaiseEvent
    End Event

    'Initialisation de la commande avec les vraies fonctions à appeller !
    Public Sub New(ByVal pexecute As Action)
        Me.New(pexecute, Nothing)
    End Sub

    Public Sub New(ByVal pexecute As Action, ByVal pcanExecute As Func(Of Boolean))
        _execute = pexecute
        _canExecute = pcanExecute
    End Sub

    'Initialisation de la commande avec les vraies fonctions à appeller, avec paramètres !
    Public Sub New(ByVal pexecuteP As Action(Of Object))
        Me.New(pexecuteP, Nothing)
    End Sub

    Public Sub New(ByVal pexecuteP As Action(Of Object), ByVal pcanExecute As Func(Of Boolean))
        _executeP = pexecuteP
        _canExecute = pcanExecute
    End Sub


End Class
