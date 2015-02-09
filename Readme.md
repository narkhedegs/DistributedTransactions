
**Distributed Transactions in .NET**
=============================

[TOC]

----------


What is a Transaction (a.k.a local transaction)?
---------------------------
Transactions bind multiple tasks together. For example, imagine that an application performs two tasks. First, it creates a new table in a database. Next, it calls a specialized object to collect, format, and insert data into the new table. These two tasks are related and even interdependent, such that you want to avoid creating a new table unless you can fill it with data. Executing both tasks within the scope of a single transaction enforces the connection between them. If the second task fails, the first task is rolled back to a point before the new table was created. To ensure predictable behavior, all transactions must possess the basic ACID properties (atomic, consistent, isolated, and durable).

What is a Distributed Transaction?
-----------------------------------------
Transactions that span multiple data sources, giving us the ability to incorporate several distinct operations occurring on different systems into a single pass or fail action, are known as Distributed Transactions.

System.Transactions
-------------------------
The System.Transactions namespace contains classes that allow you to write your own transactional application and resource manager. Specifically, you can create and participate in a transaction (local or distributed) with one or multiple participants. System.Transactions provides two models of programming:

 1. Explicit: This programming model is based on the Transaction class.
 2. Implicit: This programming model uses the TransactionScope class, in which transactions are automatically managed by the infrastructure. It is highly recommended that you use the easier implicit model for development. 

There are two key ways that the System.Transactions infrastructure provides enhanced performance.

 1. Dynamic Escalation, which means that the System.Transactions infrastructure only engages the MSDTC when it is actually required for a transaction. 
 2. Promotable Enlistments, which allows a resource, such as a database, to take ownership of the transaction if it is the only entity participating in the transaction. Later, if needed, the System.Transactions infrastructure can still escalate the management of the transaction to MSDTC. This further reduces the chance of using the MSDTC. 

Glossary
----------
**Resource Manager:** Each resource used in a transaction is managed by a resource manager, whose actions are coordinated by a transaction manager. Resource managers work in cooperation with the transaction manager to provide the application with a guarantee of atomicity and isolation. Microsoft SQL Server, durable message queues, in-memory hash tables are all examples of resource managers.

**Transaction Manager:** System.Transactions includes a transaction manager component that coordinates a transaction involving at most, a single durable resource or multiple volatile resources. Because the transaction manager uses only intra-application domain calls, it yields the best performance. Developers need not interact with the transaction manager directly. Instead, a common infrastructure that defines interfaces, common behavior, and helper classes is provided by the System.Transactions namespace. The transaction manager also transparently escalates local transactions to distributed transactions by coordinating through a disk-based transaction manager like the DTC, when an additional durable resource manager enlists itself 
with a transaction.

**MSDTC:** MSDTC is an acronym for Microsoft Distributed Transaction Coordinator. As the name says, MSDTC is a Windows service providing transaction infrastructure for distributed systems.

References
-------------

 - Transaction Fundamentals: 
    https://msdn.microsoft.com/en-us/library/z80z94hz(v=vs.110).aspx
 - ACID: https://msdn.microsoft.com/en-us/library/ms683579.aspx
 - System.Transactions: 
   https://msdn.microsoft.com/en-us/library/system.transactions(v=vs.110).aspx
 - Resource Manager:
   https://msdn.microsoft.com/en-us/library/ms229975(v=vs.110).aspx
 - Transaction Manager: 
   https://msdn.microsoft.com/en-us/library/ms229978(v=vs.110).aspx