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



#### How to Build Client Program

---

1. Dotnet Client

   1. Endpoint

      1. Let Client Reference Service Contract aka interface used in the endpoint
         1. ex: ICaculate
      2.  Choose the Binding to be used 
         1. ex: NetNamedPipeBinding (System.ServiceModel.dll)
      3. Refer Address from the endpoint

   2. Instantiate Poxy object using ChannelFactory (Built in Type in System.ServiceModel)type  

   3. ```C#
      wcf.EndpointAddress _address = new wcf.EndpointAddress("net.pipe://localhost/onmachinecchannel");
                  wcf.NetNamedPipeBinding _pipeBinding = new wcf.NetNamedPipeBinding();
      
                  //Building Proxy Object Using ChannelFactory class
                CalculatorServiceContractLib.ICalculate _proxy=
                      wcf.ChannelFactory<CalculatorServiceContractLib.ICalculate>.CreateChannel
                      (_pipeBinding, _address);
      
                  int result = _proxy.Add(10, 20);
      ```

      

2. Sharing Metadata 

   1. Let Server Publish Metadata (WSDL) using ServiceMetadataBehavior

      1. MEX Endpoints (WS- Metadata Exchange )

         1. Dedicated Endpoint to publish metadata

      2. **Publish Metadata as a Web resource (http-get resource)**

      3. ```C#
         
                     //Service Behavior to publish metadata (WSDL Document)
                     wcf.Description.ServiceMetadataBehavior _metadataBehavior = new wcf.Description.ServiceMetadataBehavior();
                     //Configure Behavior to publish metadata when ServiceHost recieves http-get request
                     _metadataBehavior.HttpGetEnabled = true;
                     //Define URL to download metadata;
                     _metadataBehavior.HttpGetUrl =new Uri( "http://localhost:8002/metadata"); // this address used only for metadata download 
         
                     //Add Behavior -> ServieHost
                     _wcfServiceHost.Description.Behaviors.Add(_metadataBehavior);
         ```

         

   2. Let Client Download Metadata and build Proxy for the same

      1. Dotnet Client
         1. **svcutil.exe** - tool to download wsdl and generates proxy code
         2. Microsoft Visual Studio-> use  Add ServiceReference  option 
         3. ![image-20200923114351050](D:\knowledge\wcf_knowledge\wcf-bootcamp-repository\Add_serviceReference)



![image-20200923114550697](D:\knowledge\wcf_knowledge\wcf-bootcamp-repository\MetdataReference)



#### Proxy Class Generated from Add ServiceReference dialog

![image-20200923114801089](D:\knowledge\wcf_knowledge\wcf-bootcamp-repository\ProxyGeneration)

### All the service endpoints - listed in Client Application  App.config file

```xml
<system.serviceModel>
        <bindings>
            <basicHttpBinding>
                <binding name="BasicHttpBinding_ICalculate" />
            </basicHttpBinding>
            <netNamedPipeBinding>
                <binding name="NetNamedPipeBinding_ICalculate" />
            </netNamedPipeBinding>
            <netTcpBinding>
                <binding name="NetTcpBinding_ICalculate">
                    <security>
                        <transport sslProtocols="None" />
                    </security>
                </binding>
            </netTcpBinding>
        </bindings>
        <client>
            <endpoint address="net.pipe://localhost/onmachinecchannel" 
                      binding="netNamedPipeBinding"
                      bindingConfiguration="NetNamedPipeBinding_ICalculate"
                      contract="CalculatorServiceReference.ICalculate"
                      name="NetNamedPipeBinding_ICalculate">
                <identity>
                    <userPrincipalName value="LAPTOP-CRJSH3D8\user" />
                </identity>
            </endpoint>
            <endpoint address="net.tcp://localhost:5000/lanep" 
                      binding="netTcpBinding"
                      bindingConfiguration="NetTcpBinding_ICalculate"
                      contract="CalculatorServiceReference.ICalculate"
                      name="NetTcpBinding_ICalculate">
                <identity>
                    <userPrincipalName value="LAPTOP-CRJSH3D8\user" />
                </identity>
            </endpoint>
            <endpoint address="http://localhost:8001/wanep" 
                      binding="basicHttpBinding"
                      bindingConfiguration="BasicHttpBinding_ICalculate"
                      contract="CalculatorServiceReference.ICalculate"
                      name="BasicHttpBinding_ICalculate" />
        </client>
    </system.serviceModel>
```

#### Use Proxy class and choose endpoint from config file

```C#
static void Main(string[] args)
        {
            //Use Generated Proxy Class
            CalculatorServiceReference.CalculateClient _proxy = 
                // Use  EndpointName  from configuration File
                new CalculatorServiceReference.CalculateClient("NetNamedPipeBinding_ICalculate");
            Console.WriteLine(_proxy.Add(10, 20));
        }
```



### Assignment

1. Build AccountService with following operation
   1. Login (userName, password)
   2. Signup (userId,username,password,email)
   3. Store User information in in memory List
   4. Publish Metadata (wsdl)

2.Build Dotnet client to test login and Signup functionality