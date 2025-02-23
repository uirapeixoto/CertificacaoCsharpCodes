<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void ExecutaTarefa(int i)
	{
	    Console.WriteLine($"Tarefa {i} comançando a trabalhar  em {DateTime.Now} ");
	    Thread.Sleep(100);
	    Console.WriteLine($"Tarefa {i} terminando a trabalhar em: {DateTime.Now}");
	}

void Main()
{
	Task tarefaMae = Task.Run(() => {
		Console.WriteLine("Tarefa mãe iniciou.");
		
		for(int i = 0; i < 10; i++){
			int tarefaFilhaId = i;
			Task filha = Task.Run( () => ExecutaTarefa(tarefaFilhaId));
		}
	});
	
	tarefaMae.Wait();
	Console.WriteLine("Tarefa mãe terminou.");
}

// Define other methods and classes here