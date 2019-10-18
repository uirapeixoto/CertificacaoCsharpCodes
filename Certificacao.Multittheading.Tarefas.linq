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

public static int CalcularResultado(int numero1, int numero2){
	Console.WriteLine("Trabalho iniciado");
	Thread.Sleep(2000);
	Console.WriteLine("Trabalho terminado");	
	Console.WriteLine();
	return numero1 + numero2;
}


public static void Tarefa1(){
	Task tarefa1 = new Task(() => ExecutaTrabalho(1));
	tarefa1.Start();
}

//Executa a tarefa colocando no pool de tarefas
public static void Tarefa2(){
	Task tarefa = Task.Run(() => ExecutaTrabalho(2));
}

//Executa a tarefa colocando no pool de tarefas
public static void Tarefa3(){
	Task<int> tarefa = Task.Run(() => {
		return CalcularResultado(2,1);
	});
	
	Console.WriteLine($"O resultado é {tarefa.Result}");
}

void Main()
{
	Tarefa1();
	Tarefa2();
	Tarefa3();
     Console.WriteLine("Término do processamento.");
       // Console.ReadLine();
    
}
// Define other methods and classes here