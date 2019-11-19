<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void Atividade03(){

	bool relogioFuncionando = true;
	
	Thread thread = new Thread(() =>
	{
	    while(relogioFuncionando)
	    {
	        Console.WriteLine("tic");
	        Thread.Sleep(1000);
	        Console.WriteLine("tac");
	        Thread.Sleep(1000);
		}
	});
	
	thread.Start();
	
	Console.WriteLine("Tecle algo para interromper.");
	Console.Read();
	
	relogioFuncionando = false;
	
	Console.ReadLine();
}

static void Atividade04(){

	CancellationTokenSource tokenSource = new CancellationTokenSource();
	Thread thread = new Thread(() =>
	{
	    while(!tokenSource.IsCancellationRequested)
	    {
	        Console.WriteLine("tic");
	        Thread.Sleep(1000);
	        Console.WriteLine("tac");
	        Thread.Sleep(1000);
		}
	});
	
	thread.Start();
	Console.WriteLine("Tecle algo para interromper.");
	Console.Read();
	tokenSource.Cancel();
	Console.ReadLine();
}

void Main()
{
	Atividade04();
	
}
// Define other methods and classes here