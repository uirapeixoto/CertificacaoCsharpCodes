<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public void ExecutaTarefa(int i)
{
    try
        {
            var message = $"Tarefa {i} comançando a trabalhar  em {DateTime.Now}";

            //Task.Factory.StartNew(() =>
            //{
            //    lock(message)
            //    {
					Thread.Sleep(500);
                    Console.WriteLine(message);
            //    }
            //});
        }
        catch (Exception ex)
        {
            throw ex;
        }
}
	
public async Task ExcutarTarefaAsync(int i)
{
	var message = $"Tarefa {i} comançando a trabalhar  em {DateTime.Now}";
	var msg = string.Empty;
	
	Console.WriteLine(message);
	Thread.Sleep(100);
 	Console.WriteLine(message);
}

void Main()
{

	for(int i = 0; i < 10; i++){
		//Task tarefaMae = Task.Run(() => {
			Console.WriteLine("Tarefa mãe iniciou.");
			ExecutaTarefa(i);
		//});
	}
	//tarefaMae.Wait();
	Console.WriteLine("Tarefa mãe terminou.");
}

// Define other methods and classes here