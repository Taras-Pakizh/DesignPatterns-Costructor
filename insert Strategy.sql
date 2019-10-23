use DesignPatterns

go

-------------------------------Default-----------------

--insert into Subjects(Name, type)
--values  ('Int', 3), ('Double', 4), ('Bool', 5), ('String', 6), ('Char', 7), 
--		('Float', 8), ('Long', 9), ('Short', 10), ('Byte', 11), ('Void', 12)

select * from Patterns
select * from Subjects
select * from SubjectReferences
select * from SubjectMethods
select * from MethodParameters
select * from SubjectProperties
-------------------------------

--insert into Patterns(Name, description)
--values ('Strategy', 'best')

--insert into Subjects(Name, pattern_Id, type)
--values ('Context', 1, 0), ('IStartegy', 1, 1), ('ConcreteStrategy1', 1, 0), ('ConcreteStrategy1', 1, 0)

--insert into SubjectReferences(subject_Id, target_Id, type)
--values (12, 11, 2), (13, 12, 3), (14, 12, 3)

--insert into SubjectMethods(AccessType, Name, ReturnValue_Id, Subject_Id)
--values  (0, 'Context', 10, 11), 
--		(0, 'ExecuteAlgorithm', 10, 11), 
--	    (0, 'Algorithm', 10, 12)

--insert into MethodParameters(method_Id, Name, type_Id)
--values (1, '_strategy', 12)

--insert into SubjectProperties(Name, Subject_Id, Type_Id)
--values ('ContextStrategy', 11, 12)