<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void ExecutaTrabalho(int i)
{
    Console.WriteLine($"Começando a trabalhar em: {i}");
    Thread.Sleep(100);
    Console.WriteLine($"Terminando a trabalhar em: {i}");
    Console.WriteLine();
}


public static void Tarefa1(){
	Task tarefa1 = new Task(() => ExecutaTrabalho(1));
	tarefa1.Start();
}

//Executa a tarefa colocando no pool de tarefas
public static void Tarefa2(){
	Task tarefa = Task.Run(() => ExecutaTrabalho(2));
}

void Main()
{
	Tarefa1();
	Tarefa2();
     Console.WriteLine("Término do processamento.");
       // Console.ReadLine();
    
}
// Define other methods and classes here