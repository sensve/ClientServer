# ClientServer
Container console echo test task

•	Python: server – located in ClientServer\Server

•	.NET: client – located in ClientServer\Client

A more detailed description can be found in the file README.docx

## How to run the program:
First you need to run the server.

### Server
1.	Run Command Prompt as administrator 
2.	Change current directory to directory where the project folder is located 

        •	Type cd {project path}

        •	In the end of the path should be …\ClientServer\Server>

3.	Run command the following command
```bash
py server.py
```
4.	After these steps the server should work. Leave the server running (don’t close the cmd window)

### Client
#### Setting up the Environment for C# Compiler
Open another Command Prompt as administrator. 
To check if the path of the C# compiler is set as Environment Variable, type csc and press Enter. 
 
If you are getting this message - 'csc' is not recognized as an internal or external command, operable program or batch file, then you need to the path of the C# compiler as Environment Variable.

#### To set the path as Environment Variable
Step 1: 

Go to Control Panel -> System. Under Advanced System Setting option click on Environment Variables.
 
 
Step 2: 

Now, you have to alter the “Path” variable under System variables so that it also contains the path to the .NET Framework environment. Select the “Path” variable and click on the Edit button.
 
 
Step 3:  

An Edit Environment variable window will appear. Let's assume that csc.exe is located in C:\Windows\Microsoft.NET\Framework\v4.0.30319 directory. This directory is the path to the compiler.
Click on the New button and add the compiler path in the text box. Then click on Ok.

 
Step 4:

Click on OK, Save the settings and it is done.
Now to check whether the environment setup is done correctly, open command prompt and type csc.
 
 
#### Steps to Execute C# Program on cmd
1.	Change current directory to directory where the project folder is located 
     
             •	Type cd {project path}
       
             •	In the end of the path should be …\ClientServer\Client>

2.	Once you are in the correct directory in which you have your program, then you can compile and run your program.
3.	To compile, type
```bash
csc program.cs
```
 
4.	Once the program is compiled, run the program by typing filename (program in our case) and then pressing Enter.
 
5.	Type something

If you look at the server cmd window, you will see all characters that you have typed in client window 
 

## How to run the program with Docker:

1.	Install the Docker 
2.	Run Command Prompt as administrator 
3.	Change current directory to directory where the project folder is located 

          •	Type cd {project path}

          •	In the end of the path should be …\ClientServer
          
4.	Open additionally Docker
 
5.	In cmd window run the following command 

```bash
docker-compose up -d && docker attach clientserver_client_1 && docker attach clientserver_server_1
```

In the Docker window you will see that containers have been created
 
 
6.	Click to clientserver_server_1 to make sure that server works
 

7.	Click clientserver_client_1 to make sure that client works
 

8.	Print something to cmd window
 
