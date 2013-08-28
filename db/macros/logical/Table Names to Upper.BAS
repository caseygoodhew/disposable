Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	TableNamesToUpper
	
	MsgBox("Done")
End Sub

Sub TableNamesToUpper
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim DefaultOwner As String

	DefaultOwner = "DISPOSABLE"

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		MyEntity.EntityName = UCase(MyEntity.EntityName)
		MyEntity.TableName = UCase(MyEntity.TableName)
	Next MyEntity
End Sub
