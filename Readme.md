**Distributed Transactions in .NET**
====================================

## What is a Transaction ?

Transactions bind multiple tasks together. For example, imagine that an application performs two tasks. First, it creates a new table in a database. Next, it calls a specialized object to collect, format, and insert data into the new table. These two tasks are related and even interdependent, such that you want to avoid creating a new table unless you can fill it with data. Executing both tasks within the scope of a single transaction enforces the connection between them. If the second task fails, the first task is rolled back to a point before the new table was created. To ensure predictable behavior, all transactions must possess the basic ACID properties (atomic, consistent, isolated, and durable).

## What is a Distributed Transaction ?

Transactions that span multiple data sources, giving us the ability to incorporate several distinct operations occurring on different systems into a single pass or fail action, are known as Distributed Transactions.

## System.Transactions

The System.Transactions namespace contains classes that allow you to write your own transactional application and resource manager. Specifically, you can create and participate in a transaction (local or distributed) with one or multiple participants. System.Transactions provides two models of programming:

 1. Explicit: This programming model is based on the Transaction class.
 2. Implicit: This programming model uses the TransactionScope class, in which transactions are automatically managed by the infrastructure. It is highly recommended that you use the easier implicit model for development. 

There are two key ways that the System.Transactions infrastructure provides enhanced performance.

 1. Dynamic Escalation, which means that the System.Transactions infrastructure only engages the MSDTC when it is actually required for a transaction. 
 2. Promotable Enlistments, which allows a resource, such as a database, to take ownership of the transaction if it is the only entity participating in the transaction. Later, if needed, the System.Transactions infrastructure can still escalate the management of the transaction to MSDTC. This further reduces the chance of using the MSDTC. 

## MSDTC

### What is MSDTC ?
MSDTC is an acronym for **Microsoft Distributed Transaction Coordinator**. MSDTC is a Windows service providing transaction infrastructure for distributed systems.

### Where is MSDTC ? 
MSDTC is a windows service and you can find it by going to - 
Administrative Tools > Services > Distributed Transaction Coordinator. 

![where is msdtc](http://i.imgur.com/F4vsRNL.png)

### How to enable MSDTC ?
You can turn MSDTC on/off by starting/stopping the Distributed Transaction Coordinator windows service.

### Where is MSDTC management tool ? 
You can find MSDTC management tool by going to - Administrative Tools > Component Services

![msdtc management tool](http://i.imgur.com/xU1JoaI.png)

### How to configure MSDTC ? 
Right click on Local DTC inside componenet services and select Properties.

![how to configure msdtc](http://i.imgur.com/EP7PdKd.png)

### MSDTC Network Configuration
To configure the Security and Network settings for MSDTC, go to Security Tab on the Local DTC properties.

![msdtc network configuration](http://i.imgur.com/Yo1oPQL.png)

*Note: Following information is taken from the Component Services Administration help file. You open this file by clicking "Learn more about setting these properties" link at the bottom of Local DTC Properties window shown in the screen shot above.*

**Network DTC Access:** Select this check box if you want to allow any network traffic for the Distributed Transaction Coordinator (DTC). If this check box is not selected, the DTC will not flow any transactions to the network, and it will not accept any incoming traffic. Remote administration of this DTC will also be disabled.

**Allow Remote Clients:** Select this check box if you want this DTC to coordinate transactions for remote clients.

**Allow Remote Administration:** Select this check box if you want to allow administration of this DTC from remote computers.

**Allow Inbound:** Select this check box to allow a remote computer to flow transactions to the local computer. Typically, this option is needed on the computer that is hosting the DTC for a resource manager such as Microsoft SQL Server.

**Allow Outbound:** Select this check box to allow the local computer to flow transactions to a remote computer. Typically, this option is needed on the client computer, where the transaction is initiated.

**Mutual Authentication Required:** If this option is selected, the local DTC (proxy or service) communicates with a remote DTC service using only encrypted messages and mutual authentication (Windows Domain authentication). If a secure communication cannot be established with the remote system, the communication is denied. This option can be used only for communication with computers running Windows Server 2003, Windows XP SP2, Windows Vista, or Windows Server® 2008.

**Incoming Caller Authentication Required:** If this option is selected, if mutual authentication cannot be established but the incoming caller can be authenticated, the communication is allowed. This option can be used only for communication with computers running Windows Server 2003 or Windows XP SP2.

**No Authentication Required:** If this option is selected, the DTC communication on the network can fall back to a nonauthenticated and nonencrypted communication if the attempts to start a secure communication fail. This option is used primarily to allow the DTC to communicate with computers running Windows 2000, Windows XP SP1, and earlier versions. This setting can also be used if one of the systems has turned off remote procedure call (RPC) security.

**Enable XA Transactions:** Select this check box to allow transactions that use the XA standard. Resource managers that run on different operating systems can communicate with a DTC transaction manager by using the XA standard.

XA interfaces are a standard set of programming interfaces that allow COM+ application developers to access XA-compliant databases and create resource managers that operate with relational databases, message queuing, transactional files, and object-oriented databases. Although Microsoft does not directly support the XA protocol, Microsoft does support translation facilities between OLE Transactions and XA. 

**DTC Logon Account:** Specify which account the DTC service runs under. 

By default, DTC runs under the Network Service account. This account is specifically designed to allow services such as the Distributed Transaction Coordinator service to run with the appropriate set of privileges. To minimize potential security problems, we recommend that you use the default Network Service account. 

If you choose to change the default DTC logon account, type the name of another account in Account, or click Browse. Then, type and confirm a password.

## Glossary

**Resource Manager:** Each resource used in a transaction is managed by a resource manager, whose actions are coordinated by a transaction manager. Resource managers work in cooperation with the transaction manager to provide the application with a guarantee of atomicity and isolation. Microsoft SQL Server, durable message queues, in-memory hash tables are all examples of resource managers.

**Transaction Manager:** System.Transactions includes a transaction manager component that coordinates a transaction involving at most, a single durable resource or multiple volatile resources. Because the transaction manager uses only intra-application domain calls, it yields the best performance. Developers need not interact with the transaction manager directly. Instead, a common infrastructure that defines interfaces, common behavior, and helper classes is provided by the System.Transactions namespace. The transaction manager also transparently escalates local transactions to distributed transactions by coordinating through a disk-based transaction manager like the DTC, when an additional durable resource manager enlists itself 
with a transaction.

## References

 - Transaction Fundamentals: 
    https://msdn.microsoft.com/en-us/library/z80z94hz(v=vs.110).aspx
 - ACID: https://msdn.microsoft.com/en-us/library/ms683579.aspx
 - System.Transactions: 
   https://msdn.microsoft.com/en-us/library/system.transactions(v=vs.110).aspx
 - Resource Manager:
   https://msdn.microsoft.com/en-us/library/ms229975(v=vs.110).aspx
 - Transaction Manager: 
   https://msdn.microsoft.com/en-us/library/ms229978(v=vs.110).aspx
 - MSDTC Tracing: http://blogs.msdn.com/b/distributedservices/archive/2009/02/07/the-hidden-tool-msdtc-transaction-tracing.aspx
 - Transaction Scope Options: https://msdn.microsoft.com/en-us/library/ms172152(v=vs.110).aspx