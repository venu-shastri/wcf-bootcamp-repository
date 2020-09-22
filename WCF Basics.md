### WCF Basics

---

- FrameWork (set of Libraries)
  - System.ServiceModel.dll
  - ![image-20200922120156100](D:\knowledge\wcf_knowledge\reference)

- WCF provides infrastructure to publish CLR object as a service

  - Messaging Layer

- WCF Building Blocks

  - Service : "Class / Instance"

  - ServiceContract

  - OperationContract

  - Behaviors

    - ServiceBehaviors
    - Operation Behaviors

  - ServiceHost :- Host for Service , One host instance / Service Type

    - Supports Multiple endpoints

  - Endpoint

    - Contract - "List Of Operations Published from service" - What ?
    - Binding - "How " - Transport + Encoder + MessageFormatter +Security +Transaction
    - Address = "Location" - Network Address of Service

    

    ### WCF Steps

    ----

    1. Create Service

       ```c#
       //Service Lib - CalculatorLib.dll
       public class Calculator
           {
               public int Add(int x,int y)
               {
                   return x + y;
               }
           }
       ```

    2. Host Service

       

    ```C#
    //Server Program - TestServer.exe
    using wcf=System.ServiceModel
        
     //Main
    			//CalculatorLib.Calculator _service = new CalculatorLib.Calculator();
                //Host service or Service Instance
               // wcf.ServiceHost _wcfServiceHost = new wcf.ServiceHost(_service);
        wcf.ServiceHost _wcfServiceHost = new wcf.ServiceHost(typeof(CalculatorLib.Calculator));
                _wcfServiceHost.Open();// ServiceHost availability for Client Requests
    ```

    

    3.  Add Endpoint

       1. Define Contract : interface of Service

          ```C#
          
              [System.ServiceModel.ServiceContract] //WCF
              public interface ICalculate
              {
                  [System.ServiceModel.OperationContract] //WCF 
                  int Add(int x, int y);
                  int sub(int x, int y);
              }
          ```

       2.  Choose Binding 

          1. Communication Channel to be used for message exchange

             1. Transport : Message Carrier
             2. Formatter : Serialization / Deserialization
             3. Encoder : text/ binary
             4. Policy : Security , Encryption , Transaction

          2. WCF provides Built in bindings

             1. How to choose One ?
                1. List out Operational Requirements
                   1. Client Runtime
                   2. Client Location
                   3.  specific Operational rules

             ```
             Binding = Communication Channel / Operational Requirements
             ```

          3. Ex:-

             | Opertional -ReQ            | Communication-Channel -> | Transport  | Encoder | Formatter    |
             | -------------------------- | ------------------------ | ---------- | ------- | ------------ |
             | Client Runtime             | .Net                     |            | Binary  | CLR Specific |
             | Client Location            | Local Machine            | NamedPipes |         |              |
             | Specific Operational Rules | Request/ Reply           |            |         |              |
             |                            |                          |            |         |              |

             

    |                            |                | Transport | Encoder | Formatter    |
    | -------------------------- | -------------- | --------- | ------- | ------------ |
    | Client Runtime             | .Net           |           | Binary  | CLR Specific |
    | Client Location            | LAN            | TCP       |         |              |
    | Specific Operational Rules | Request/ Reply |           |         |              |
    |                            |                |           |         |              |

    |                            |                | Transport | Encoder | Formatter    |
    | -------------------------- | -------------- | --------- | ------- | ------------ |
    | Client Runtime             | .Net           |           | Text    | CLR Specific |
    | Client Location            | WAN            | Http      |         |              |
    | Specific Operational Rules | Request/ Reply |           |         |              |
    |                            |                |           |         |              |

    

|                            |                        | Transport | Encoder | Formatter |
| -------------------------- | ---------------------- | --------- | ------- | --------- |
| Client Runtime             | Java                   |           | Text    | XML/JSON  |
| Client Location            | Local Machine /LAN/WAN | Http      |         |           |
| Specific Operational Rules | Request/ Reply         |           |         |           |
|                            |                        |           |         |           |

3. Choose Addreess
   1. Select protocol based on - Bindings
      1. ex: NetNamedpipeBinding - "net.pipe://"
      2. ex: NetTcpBinding - "net.tcp://"
      3. ex: BasicHttpBinding : "http://"

```C#
 System.Type _contract = typeof(CalculatorServiceContractLib.ICalculate);
            //Bidning
            wcf.NetNamedPipeBinding _localMachineBidning = new wcf.NetNamedPipeBinding();
            //Address 
            string address = "net.pipe://localhost/onmachinecchannel";

            //wcf.Description.ServiceEndpoint _localMachineCommunicationChannel = 
            //    new wcf.Description.ServiceEndpoint(
            //    new wcf.Description.ContractDescription(_contract.FullName),
            //    _localMachineBidning, 
            //    new wcf.EndpointAddress(address)
            //    );
            //_wcfServiceHost.AddServiceEndpoint(_localMachineCommunicationChannel);
            _wcfServiceHost.AddServiceEndpoint(_contract, _localMachineBidning, address);

            //LAN Clients
            _wcfServiceHost.AddServiceEndpoint(_contract, new NetTcpBinding(), "net.tcp://localhost:5000/lanep");
            //WAN
            _wcfServiceHost.AddServiceEndpoint(_contract, new BasicHttpBinding(), "http://localhost:8001/wanep");
```

