Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	SetTableOwner
	
	MsgBox("Done")
End Sub

Sub SetTableOwner
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim DefaultOwner As String

	DefaultOwner = "DISPOSABLE"

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		If MyEntity.Owner = "" Then
			MyEntity.Owner = DefaultOwner
		End If
	Next MyEntity
End Sub
