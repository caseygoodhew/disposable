Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	NamesUppercase
	
	MsgBox("Done")
End Sub

' this doesn't work becuse it doesn't set FK role names to uppercase
Sub NamesUppercase
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyColumn As AttributeObj
	Dim MyIndex As Index
	Dim MyRelation As Relationship
	Dim MyFKColumnPair As FKColumnPair

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		MyEntity.EntityName = UCase(MyEntity.EntityName)
		MyEntity.SystemName = UCase(MyEntity.SystemName)
		MyEntity.TableName = UCase(MyEntity.TableName)

		For Each MyColumn In MyEntity.Attributes
			MyColumn.AttributeName = UCase(MyColumn.AttributeName)
			MyColumn.CheckConstraintName = UCase(MyColumn.CheckConstraintName)
			MyColumn.ColumnName = UCase(MyColumn.ColumnName)
			MyColumn.DefaultName = UCase(MyColumn.DefaultName)
			MyColumn.RoleName = UCase(MyColumn.RoleName)
			MyColumn.SystemName = UCase(MyColumn.SystemName)
			MyColumn.Title = UCase(MyColumn.Title)
		Next

		For Each MyIndex In MyEntity.Indexes
			MyIndex.Name = UCase(MyIndex.Name)
		Next
	Next MyEntity

	For Each MyRelation In MyModel.Relationships
		MyRelation.BusinessName = UCase(MyRelation.BusinessName)
		MyRelation.Name = UCase(MyRelation.Name)

		For Each MyFKColumnPair In MyRelation.FKColumnPairs
			MyFKColumnPair.ChildAttribute.RoleName = UCase(MyFKColumnPair.ChildAttribute.RoleName)
		Next
	Next
End Sub
