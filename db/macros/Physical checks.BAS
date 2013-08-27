'DO NOT MODIFY THIS FILE DIRECTLY
Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	If MyModel.Name <> "MYSQL" Then
			'Err.Raise vbObjectError+1, "", "This can only be applied when the physical model is active"
			MsgBox("This can only be applied when the physical model is active")
			Exit Sub
	End If
	
	SetAllTableTypesToInnoDB
	
	MsgBox("Done")
End Sub

'DO NOT MODIFY THIS FILE DIRECTLY
Sub SetAllTableTypesToInnoDB
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		MyEntity.MySQLTableType = "INNODB"
	Next
End Sub
