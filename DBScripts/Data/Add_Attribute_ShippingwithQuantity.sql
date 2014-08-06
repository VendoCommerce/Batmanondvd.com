declare @ObjectId int
declare @AttributeId int
declare @SkuWeightAttributeId int
declare @objectattributeid int
declare @AttributeName nvarchar(50)
declare @ObjectAttributeDesc nvarchar(500)
declare @ValueTypeId int
declare @ObjectAttributeTypeId int

-- set object name
select @ObjectId = (select objectid from objects where name = 'SKU')

-- set attribute name
set @AttributeName = 'ShippingwithQuantity'

-- set object attribute desc
set @ObjectAttributeDesc = 'ShippingwithQuantity.'

-- set type of value ('bool' or 'text1')
set @ValueTypeId = (select ValueTypeId from ValueTypes where name = 'bool')

-- set type of control ('default' or 'rich-control')
set @ObjectAttributeTypeId = (select ObjectAttributeTypeId from ObjectAttributeTypes where name = 'default')

select @AttributeId = (select attributeid from attributes where name = @AttributeName)
if (@AttributeId is null)
begin
	insert into attributes (name, [description], defaultvaluetypeid) 
	values (@AttributeName, @AttributeName, @ValueTypeId)	
	
	set @AttributeId = SCOPE_IDENTITY()
end
else
begin
	update attributes
	set name = @AttributeName,
		[description] = @AttributeName,
		defaultvaluetypeid = @ValueTypeId
	where AttributeId = @AttributeId
end

select @objectattributeid = (select objectattributeid from ObjectAttributes where attributeid = @AttributeId and objectid = @ObjectId)

if (@objectattributeid is null)
begin
	insert into ObjectAttributes (objectid, attributeid, objectattributetypeid, [description], DisplayLabel) 
	values (@ObjectId, @AttributeId, @ObjectAttributeTypeId, @ObjectAttributeDesc, @AttributeName)
end
else
begin
	update ObjectAttributes
	set objectid = @ObjectId,
		attributeid = @AttributeId,
		DisplayLabel = @AttributeName,
		objectattributetypeid = @ObjectAttributeTypeId,
		[description] = @ObjectAttributeDesc
	where objectattributeid = @objectattributeid
end
