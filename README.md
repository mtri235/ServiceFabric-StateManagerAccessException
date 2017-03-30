# Bug Description
This project reproduces an issue
where System.Collections.ListDictionaryInternal throw System.ExceptionResource
after replica is restarted.
# Reproduction Steps
1. Set StateManagerAccess as start up project
2. Run in debug mode - make sure all project run with x64
3. Right click on Shopping Console project and choose Debug > Start new instance
4. Press enter to setup Products and Shopping Cart with test data
5. Enter "l" to list all product in shopping cart to validate data is setup correctly
6. Keep console open, restart replica with the following powershell commands
```
$ Connect-ServiceFabricCluster
$ Restart-ServiceFabricPartition -ServiceName fabric:/StateManagerAccess/ShoppingCartActorService -RestartPartitionMode AllReplicasOrInstances
```
7. Go back to console, enter "l" to list product in shopping cart 
 