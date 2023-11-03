# QTLSecure
## About
> QTLSecure is just a console application designed to take the functionalities from the QuickTools Library and inplement an easy to use inplamantation of the QuickTools.Security.Secure class which includes multiple methods in it but the ones implemented in it are the EncryptFile and DecrypFile along with the functionalities of allowing multiple files encription and also command line parameters for the easy use of the application
> <img width="325" alt="image" src="https://github.com/Mel4221/QTLSecure/assets/87794877/b0b492de-3c01-4bc4-8bdb-ad6207587dd8">
## How To use
### From the Terminal
> From the terminal you could simply just execute the program but
where you could take advantage the most is with the easy to use arguments that
you bould pass to it as an example:
The console only expect 5 Arguments and 2 of them are optionals example:

> Example 1: <img width="206" alt="image" src="https://github.com/Mel4221/QTLSecure/assets/87794877/dba9632f-13c4-4f59-8bd2-ff52d45a5d6a">

> Example 2: <img width="437" alt="image" src="https://github.com/Mel4221/QTLSecure/assets/87794877/78c18636-0227-4c3b-92cd-e050fa78d25e">

### Simply executing the Program: 
> by just dobule click in the app and fallowing the fallowing the steps that it ask you for

### Video Tutorial: 
> https://youtu.be/xuvHJhX2Vtw

### Text Tutorial
Quick Tutorial on how to use the 
QTLSecure Program: 

The Program could be used on 2 Simple wasy

1. Clicking on it and fallowing the steps
	
	Encrypting: 
	To encrypt a file you just need the fallowing things

	File:  the file to encrypt
	Password: The password to encrypt the file
	"IV" could be omitted by typing 'null' but if you type anything else you will have to remember what that was otherwise you won't be able to decrypt the file 
	OutFile: this will be the output file if you type "same" it will override the original file
	and by default is set to encrypted_{name of the file}
	
		Examples in the Video: 
	Decrypting: 
	
	This Requires literally the same steps from encrypting but remember to select the file that is encrypted because it could fail if the file selected is the incorrect one 
	
	
2. Passing Arguments from the terminal 

	Encrypting: 
		To encrypt a file from the terminal is almost the same than fallwoing the steps the only difference here will be the way and the oreder of the comands an example will be the fallowing: 

	            ./RunProgram    [ File ] [ Mode ] [ Password ] [ IV ] [ OutFile ]
		          ./QTLSecure.exe file.txt -encrypt 1234 null encrypted_file.txt

		The arguments to encrypt are very simple: 
		File: File to encrypt not biger than 100MB Prefered
		Password: the password to encrypt the file 
		Mode:  -e,-encrypt,encrypt:
		IV: "null" or anything else but remember that you can not forget it.
		OutFile: set the file to output 

	
	Decrypting 
		Literally the same the only thing that changes are the Modes: 

		
		The arguments to encrypt are very simple: 
		File: File to encrypt not biger than 100MB Prefered
		Password: the password to encrypt the file 
		Mode:  -d , decrypt,-decrypt
		IV: "null" or anything else but remember that you can not forget it.
		OutFile: set the file to output 

	Finally one extra function is to encyrpt all the file in a directory which uses the method of overritting all the files in the given directory also there will be a video showing how to do it but i will type an example from the terminal and in the video there will be an example on how to do it too. 

		Encrypting All the files in a directory: 
			
			   ./RunProgram [ File ] [ Mode ] [ Password ] [ IV ] [ OutFile ]
			./QTLSecure.exe privatedocs -encrypt-all 1234 null same
			
		in this option is required the option same anything else will be ignored 
			
		Decrypting almost everyting the same but as always the Mode must be differen
			 ./RunProgram [ File ] [ Mode ] [ Password ] [ IV ] [ OutFile ]
			./QTLSecure.exe privatedocs -decrypt-all 1234 null same

		


 
