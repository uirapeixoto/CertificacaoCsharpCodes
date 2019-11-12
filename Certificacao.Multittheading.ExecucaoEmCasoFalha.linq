<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public void Metodo1() => Console.WriteLine("Retorno do método 1");
public void Metodo2() => Console.WriteLine("Retorno do método 2");
public void Metodo3() => Console.WriteLine("Retorno do método 3");


void Main()
{
	Task tarefa = Task.Run(() => Metodo1());
	tarefa.ContinueWith((tarefa2) => Metodo2(), TaskContinuationOptions.OnlyOnFaulted);
	tarefa.ContinueWith((tarefa3) => Metodo3(), TaskContinuationOptions.NotOnFaulted);
      
}
// Define other methods and classes here