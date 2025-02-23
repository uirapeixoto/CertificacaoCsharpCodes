<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

class MyClass
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public DateTime Data {get; set;}
}

public void Metodo1() => Console.WriteLine("Retorno do método 1");
public void Metodo2()
{
	throw new Exception("Erro");
}

static void Executar()
{
    Console.WriteLine("Início da execução");
    Thread.Sleep(1000);
    Console.WriteLine("Fim da execução");
}

static void ExecutarComParametro(object param)
{
    Console.WriteLine($"Início da execução: {param}");
    Thread.Sleep(1000);
    Console.WriteLine("Fim da execução: {0}", param);
}

public void TaskVersusThread(){
	//1. Task X Thread
    Thread thread1 = new Thread(Executar);
	thread1.Name = "Thread 01";
    thread1.Start();
	thread1.Join();
}

public void ThreadComExpressaoLambda(){
	//2. Thread com expressão lambda
    Thread thread2 = new Thread(() => Executar());
	thread2.Name = "2. Thread com expressão lambda";
    thread2.Start();
	thread2.Join();
}

public void ThreadComParametro(){
	//3. Passando parametro para a Thread
		var myClass = new MyClass{
			Id = 1, 
			Nome = "Uirá",
			Data = DateTime.Now
		};
		
		ParameterizedThreadStart ps = new ParameterizedThreadStart((p) => ExecutarComParametro(myClass.Nome));
	
	    Thread thread3 = new Thread(ps);
		thread3.Name = "3. Passando parametro para a Thread";
		ExibirThread(thread3);
	    thread3.Start();
		thread3.Join();
}

public void InterrompendoRelogio(){
//4. Interrompendo um relógio
	bool relogioFuncionando = true;
    Thread thread4 = new Thread(() =>
    {
		while(relogioFuncionando){
	        Console.WriteLine("tic");
	        Thread.Sleep(1000);
	        Console.WriteLine("tac");
	        Thread.Sleep(1000);
		}
    });
	thread4.Name = "4. Interrompendo um relógio";
    thread4.Start();
	Console.WriteLine("Tecla um tecla para interromper o relógio");
	Console.Read();
	relogioFuncionando = false;
}

public void InformacoesThread(){
	//6. Dados da Thread: Nome, cultura, prioridade, contexto, background, pool
	Thread thread6 = new Thread(Executar);
	ExibirThread(thread6);
	thread6.Start();

}


//Dados da Thread: Nome, cultura, prioridade, contexto, background, pool
static void ExibirThread(Thread t){
	Console.WriteLine();
	Console.WriteLine($"Nome: {t.Name}");
	Console.WriteLine($"Cultura: {t.CurrentCulture}");
	Console.WriteLine($"Prioridade: {t.Priority}");
	Console.WriteLine($"Contexto: {t.ExecutionContext}");
	Console.WriteLine($"Background: {t.IsBackground}");
	Console.WriteLine($"Pool: {t.IsThreadPoolThread}");
}

static void ExecutaPool(int valor){
	
	for (int i = 0; i < valor ; i++)
    {
        int estadoDoItem = i;
        ThreadPool.QueueUserWorkItem((estado)
            => ExecutarComParametro(estadoDoItem));
    }
	Console.ReadLine();
}

void Main()
{
	ExibirThread(Thread.CurrentThread);
	InformacoesThread();
}
// Define other methods and classes here