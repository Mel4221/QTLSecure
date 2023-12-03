using QuickTools.QConsole;
using QuickTools.QIO; 
using QuickTools.QCore;
using QuickTools.QSecurity;
using QuickTools.QData;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace QTLSecure
{
    public class Program
    {
        public static string CurrentJobStatus;

        static void PrintHelp(){
			Get.Yellow("The Format is the fallowing: ");
			Get.Yellow("Encription");
			Get.White();
			Console.Write("File ");
			Get.Green();
			Console.Write("-e ");
			Get.Blue();
			Console.Write("Password ");
			Get.Yellow();
			Console.Write("IV ");
			Get.Red();
			Console.Write("OutFile");
			Get.WriteL("");
			Get.Yellow("Decrypting");
			Get.White();
			Console.Write("File ");
			Get.Green();
			Console.Write("-d ");
			Get.Blue();
			Console.Write("Password ");
			Get.Yellow();
			Console.Write("IV ");
			Get.Red();
			Console.Write("OutFile");
			Console.WriteLine("\n");
            Get.White();
			Get.WriteL("File: The fist argument is the file that you wish to encrypt");
			Get.Green();
			Get.WriteL("-e or -d: \n-e = Encrypt\n-d = Decrypt\nSo if you want to encrypt just use [-e] and for decrypt [-d]");
            Get.WriteL("Some special modes are also :\n -encrypt-all \n encrypt-all \n -e-all \n And almost the smae for " +
            "Decrypting it: -decrypt-all \n decrypt-all \n -d-all");
            Get.Blue();
			Get.WriteL("Password: this argument is the password on plain text for the file");
			Get.Yellow();
			Get.WriteL("VI: Stands for the vector for the password you could use the same password\nhere or if you wish you could just type null \nand it will auto generate one for you");
			Get.WriteL("Recomended Argument: null");
			Get.Red("OutFile: is the output file for the encryption \navoid providing the same \n file just in case something goes wrong but if you don't mine");
			Get.Red("Recomended Argument: same");
			Get.Reset();

			Get.White("Examples: ");
			Get.Yellow("Encription");
			Get.White();
			Console.Write("File.txt ");
			Get.Green();
			Console.Write("-e ");
			Get.Blue();
			Console.Write("securePassword1234 ");
			Get.Yellow();
			Console.Write("null ");
			Get.Red();
			Console.Write("New_File.txt");
			Get.WriteL("");
			Get.Yellow("Decrypting");
			Get.White();
			Console.Write("New_File.txt ");
			Get.Green();
			Console.Write("-d ");
			Get.Blue();
			Console.Write("securePassword1234 ");
			Get.Yellow();
			Console.Write("null ");
			Get.Red();
			Console.Write("Decrypted_File.txt");
			Console.WriteLine("\n");
			Console.ResetColor();
		}
        static void Main(string[] args)
        {
            Get.Title("QTLSecure v3");
            try
            {
                Secure secure = new Secure();
                FilesMaper maper = new FilesMaper();
                List<Error> error = new List<Error>();
                List<string> files = maper.Files;
                Check check;
                string mode, password, iv, outFile, path, file, str;
                int current, goal;
                secure.AllowDebugger = true;

                if (args.Length == 0)
                {
                    string[] list = { "Encrypt", "Decrypt","Encrypt Entire Directory","Decrypt Entired Directory","Exit" };
                    Options option = new Options(list);

                    while (true)
                    {


                        switch (option.Pick())
                        {
                            case 0:
                                /*Encrypt*/
                                file = Get.Input("File").Text;

                                secure.OutFile = Get.Input("Output File").Text == "" ? $"encrypted_{file}" : Get.Text;
                                try
                                {
                                    secure.EncryptFile(file, Get.Password("Password", true), secure.CreatePassword(Get.Input("IV").Text=="" ? "NULL" : Get.Text));
                                    Get.Wait();
                                }
                                catch(Exception ex)
                                {
                                    Get.Red($"Something Went Wrong While Encrypting the file, more info: \n{ex}");
                                    Get.Wait();
                                }
                                break;
                            case 1:
                                /*Decrypt*/

                                file = Get.Input("File").Text;
                                secure.OutFile = Get.Input("Output File").Text == "" ? $"decrypted_{file}" : Get.Text;
                                try
                                {
                                    secure.DecryptFile(file, Get.Password("Password"), secure.CreatePassword(Get.Input("IV").Text=="" ? "NULL" : Get.Text));
                                    Get.Wait();
                                }
                                catch (Exception ex)
                                {
                                    Get.Red($"Something Went Wrong While Decrypting the file, more info: \n{ex}");
                                }
                                    break;
                            case 2:
                                path = Get.Input("Paste or type the Directory Path: ").Text;
                                mode = "encrypt-all";
                                password = Get.Password("Password", true);
                                iv = Get.Input("IV").Text=="" ? "NULL" : Get.Text;
                                outFile = "same";
                                Main(new string[] {path,mode,password,iv,outFile});
                                Get.Wait();
                                return;
                                break;
                            case 3:
                                path = Get.Input("Paste or type the Directory Path: ").Text;
                                mode = "decrypt-all";
                                password = Get.Password("Password");
                                iv = Get.Input("IV").Text=="" ? "NULL" : Get.Text;
                                outFile = "same";
                                Main(new string[] { path,mode, password, iv, outFile });
                                Get.Wait();
                                break;
                            default:
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
                else
                {
                    if(args.Length == 1)
                    {
                        PrintHelp();
                        return;
                    }
                    if (args.Length == 5)
                    {
                        
                        file = args[0];
                        mode = args[1];
                        password = args[2];
                        iv = args[3];
                        outFile = args[4];
                        path = file;
                        current =0;
                        goal = 0; 
                        //file -e password null same
                        if (!File.Exists(file) && !Directory.Exists(path))
                        {
                            Get.Red($"The File or Directory : '{file}' does not exist or could not be found");
                            return;
                        }
                        switch (mode)
						{//-encrypt-all \n encrypt-all \n -e-all"
							case "-encrypt-all":
                            case "encrypt-all":
                            case "-e-all":
                                    
                                    maper.Path = path; 
                                    maper.AllowDebugger = true;
                                    maper.Map();
                                    files = maper.Files;
                                    check = new Check();
                                    check.Start(); 
                                    Get.Yellow($"Files In Directory: {files.Count}");
                                    Get.WaitTime(1000);
                                    goal = files.Count -1;
                                    secure.PrintingTime = 10;
                                for (int item = 0; item < files.Count; item++)
                                {
                                    try
                                    {
                                        file = files[item];
                                        secure.OutFile = file;//outFile == "same" ? file : outFile;
                                        secure.AllowDebugger = true;
                                        try
                                        {
                                            secure.EncryptFile(file, password, secure.CreatePassword(iv=="null" ? "NULL" : iv));
                                        }
                                        catch(Exception ex)
                                        {
											string msg = $"The File: {files[item]} Fail To Be encrypted";
											Get.Red(msg);
											error.Add(new Error()
											{
												Message = ex.Message,
												Type = msg
											});
										}
                                        current =item;
                                        str = $"Current Status: {Get.Status(current, goal)}";
                                        CurrentJobStatus = str;
                                        Get.Title(str);
                                       
                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = $"The File: {files[item]} Fail To Be encrypted";
                                        Get.Red(msg);
                                        error.Add(new Error() { 
                                            Message = ex.Message,
                                            Type = msg
                                        });

                                    }
                                }
                                foreach (Error err in error) {
                                    Log.Event("Failed_ToBe_Encrypted", err.ToString());
                                Get.Red($"{err.ToString()}"); 
                                }
                                
                                Get.Green($"{files.Count - error.Count} Files Sucessfully Encrypted!!!");
                                                               if (error.Count > 0) Get.Red($"Failled: {error.Count}");

                                Get.Yellow($"Time: {check.Stop()}");
                                Console.Beep();

                                break;
                            case "-decrypt-all":
                            case "decrypt-all":
                            case "-d-all":
                                maper.Path = path;
                                maper.AllowDebugger = true;
                                maper.Map();
                                files = maper.Files;
                                check = new Check();
                                check.Start();
                                Get.Yellow($"Files In Directory: {files.Count}");
                                Get.WaitTime(1000);
                                goal = files.Count -1;
                                secure.PrintingTime = 10;
                                for (int item = 0; item < files.Count; item++)
                                {
                                    try
                                    {
                                        file = files[item];
                                        secure.OutFile = outFile == "same" ? file : outFile;
                                        secure.AllowDebugger = true;
                                        try
                                        {
                                            secure.DecryptFile(file, password, secure.CreatePassword(iv=="null" ? "NULL" : iv));
										}
										catch (Exception ex)
										{
											string msg = $"The File: {files[item]} Fail To Be decrypted";
											Get.Red(msg);
											error.Add(new Error()
											{
												Message = ex.Message,
												Type = msg
											});

										}
										current =item;
                                        str = $"Current Status: {Get.Status(current, goal)}";
                                        CurrentJobStatus = str;
                                        Get.Title(str);
                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = $"The File: {files[item]} Fail To Be decrypted";
                                        Get.Red(msg);
                                        error.Add(new Error()
                                        {
                                            Message = ex.Message,
                                            Type = msg
                                        });

                                    }
                                }
                                foreach (Error err in error)
                                {
									Log.Event("Failed_ToBe_Decrypted", err.ToString());

									Get.Red($"{err.ToString()}"); 
                                }
                                Get.Green($"{files.Count - error.Count} Files Sucessfully Decrypted!!!");
                                if (error.Count > 0) Get.Red($"Failled: {error.Count}");
                                Get.Yellow($"Time: {check.Stop()}");
                                Console.Beep();

                                break;
                            case "-e":
                            case "encrypt":
                            case "-encrypt":
                                secure.OutFile = outFile == "same" ? file : outFile;
                                secure.EncryptFile(file, password, secure.CreatePassword(iv=="null" ? "NULL" : iv));
                                break;
                            case "-d":
                            case "decrypt":
                            case "-decrypt":
                                secure.OutFile = outFile == "same" ? file : outFile;
                                secure.DecryptFile(file, password, secure.CreatePassword(iv=="null" ? "NULL" : iv));
                                 
                                break;
                            default:
                                Get.Red($"Incorrect Mode: {mode}");
                                Get.Yellow("The only modes available are -e or -d or the longer words [encrypt] and [decrypt]");
                                break;
                        }
                        //file -d password null same
                        //Get.Wait();
                    }
                    else
                    {
                        Get.Yellow($"Incorrect Amount of arguments given");
                        PrintHelp();
                        return;
                    }


                }
            }catch(Exception ex)
            {
                Get.Red($"It looks that something went really wrong , more info:");
                Get.Yellow(ex); 
            }
        }
    }
}
