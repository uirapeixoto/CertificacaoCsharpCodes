<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public void Metodo1() => Console.WriteLine("Retorno do método 1");
public void Metodo2()
{
	throw new Exception("Erro");
}

public void Factory(){

var parent = Task.Factory.StartNew(() => {
         Console.WriteLine("Outer task executing.");

         var child = Task.Factory.StartNew(() => {
            Console.WriteLine("Nested task starting.");
            Thread.SpinWait(500000);
            Console.WriteLine("Nested task completing.");
         });
      });

      parent.Wait();
      Console.WriteLine("Outer has completed.");
}

public void Metodo3() => Console.WriteLine("Retorno do método 3");

//Você tem o seguinte fluxograma, onde os métodos 2 e 3 precisam ser executados somente se o método 1 foi executado com sucesso ou falha, respectivamente:
/*
	                     +
	                     |
	                     v
	                +---------+
	                | Método1 |
	                +---------+
	                     |
	                     |
	          S          v           N
	     +--------+ ( Sucesso ? )+--------+
	     |                                | 
	     |                                |
	+----v----+                      +----v----+
	| Método2 |                      | Método3 |
	+---------+                      +---------+
	Cada um dos métodos precisa ser executado em uma tarefa (Task)
	
*/
public void VerificaTarefa(){
	
	Task tarefa = Task.Run(() => Metodo1());
	tarefa.ContinueWith((tarefa2) => Metodo2(), TaskContinuationOptions.OnlyOnFaulted);
	tarefa.ContinueWith((tarefa3) => Metodo3(), TaskContinuationOptions.NotOnFaulted);
	
	var cont = Process.GetCurrentProcess().Threads.Count;
	Console.WriteLine(cont);
}


void Main()
{
	Factory();
}
// Define other methods and classes here