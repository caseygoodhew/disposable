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
	SetAllIntIdentityColumnsToUnsigned
	
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

'DO NOT MODIFY THIS FILE DIRECTLY
Sub SetAllIntIdentityColumnsToUnsigned

	Const MAXLOOPS = 20

	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyColumn As AttributeObj
	ReDim ColumnsFollowed(0) As Integer
	ReDim ToSet(0) As String
	Dim FoundQualifiedName As Boolean
	Dim QualifiedName As String
	Dim SetCount As Integer
	Dim LoopCount As Integer
	Dim i As Integer

	LoopCount = 0
	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	Do
		ReDim ColumnsFollowed(0)
		ReDim ToSet(0)
		SetCount = 0

		'generate the columns to set
		For Each MyEntity In MyModel.Entities
			For Each MyColumn In MyEntity.Attributes
				If (MyColumn.Identity And MyColumn.Datatype = "INT") Or MyColumn.Unsigned Then
					Call SetAllIntIdentityColumnsToUnsigned_SetUnsigned(ColumnsFollowed, ToSet, MyModel, MyEntity, MyColumn)
				End If
			Next
		Next
	
		'set the columns that need to be set based on their fully qualified names
		For Each MyEntity In MyModel.Entities
			For Each MyColumn In MyEntity.Attributes
				
				FoundQualifiedName = False
				QualifiedName = SetAllIntIdentityColumnsToUnsigned_GenerateQualifiedName(MyEntity, MyColumn)
				i = 1
	
				Do While FoundQualifiedName = False And i < UBound(ToSet)
					If ToSet(i) = QualifiedName And Not MyColumn.Unsigned Then
						FoundQualifiedName = True
						MyColumn.Unsigned = True
						SetCount = SetCount + 1
					End If
					i = i + 1
				Loop
			Next
		Next

		LoopCount = LoopCount + 1
	'we don't get them all on the first round due to column id mismatching, so lets keep going until
	'we either don't set anyting new, or MAXLOOPS loops because more than that is probably too much
	Loop Until SetCount = 0 Or LoopCount = MAXLOOPS

	If SetCount > 0 Then
		MsgBox("SetAllIntIdentityColumnsToUnsigned exited prematurly after " & MAXLOOPS & " loops")
	End If

End Sub
'DO NOT MODIFY THIS FILE DIRECTLY
Sub SetAllIntIdentityColumnsToUnsigned_SetUnsigned(ByRef ColumnsFollowed() As Integer, ByRef ToSet() As String, MyModel As Model, MyEntity As Entity, MyColumn As AttributeObj)

	Dim MyRelationship As Relationship
	Dim MyFKColumnPair As FKColumnPair
	Dim QualifiedName As String
	Dim FoundQualifiedName As Boolean
	Dim i As Integer

	'FK columns and table columns don't have the same column ids, so we need to track the columns to be updated by fully qualified name (i can't find a better way)
	'At the same time, we need to make sure that we're not traversing over a column more than once (circular ref check)
	'This may not be necessary, but it's better to be safe than sorry (and loose work)
	For i = 1 To UBound(ColumnsFollowed)
		If ColumnsFollowed(i) = MyColumn.ID Then
			Exit Sub
		End If
	Next i

	ReDim Preserve ColumnsFollowed(UBound(ColumnsFollowed) + 1)
	ColumnsFollowed(UBound(ColumnsFollowed)) = MyColumn.ID

	FoundQualifiedName = False
	QualifiedName = SetAllIntIdentityColumnsToUnsigned_GenerateQualifiedName(MyEntity, MyColumn)
	
	'check to see if we're already checking this column by name (don't double up)
	For i = 1 To UBound(ToSet)
		If ToSet(i) = QualifiedName Then
			FoundQualifiedName = True
			Exit For
		End If
	Next i

	If Not FoundQualifiedName Then
		ReDim Preserve ToSet(UBound(ToSet) + 1)
		ToSet(UBound(ToSet)) = QualifiedName
	End If

	' traverse relationships
	For Each MyRelationship In MyModel.Relationships
		If MyRelationship.ParentEntity.ID = MyEntity.ID Then
			For Each MyFKColumnPair In MyRelationship.FKColumnPairs
				If MyFKColumnPair.ParentAttribute.ID = MyColumn.ID Then
					Call SetAllIntIdentityColumnsToUnsigned_SetUnsigned(ColumnsFollowed, ToSet, MyModel, MyRelationship.ChildEntity, MyFKColumnPair.ChildAttribute)
				End If
			Next
		End If
	Next

End Sub
'DO NOT MODIFY THIS FILE DIRECTLY
Function SetAllIntIdentityColumnsToUnsigned_GenerateQualifiedName(MyEntity As Entity, MyColumn As AttributeObj)
	SetAllIntIdentityColumnsToUnsigned_GenerateQualifiedName = Chr(34) & MyEntity.Owner & Chr(34) & "." & Chr(34) & MyEntity.EntityName & Chr(34) & "." & Chr(34) & MyColumn.RoleName & Chr(34)
End Function

