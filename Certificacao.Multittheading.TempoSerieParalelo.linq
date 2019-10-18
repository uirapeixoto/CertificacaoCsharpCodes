<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.Parallel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Timer.dll</Reference>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void CozinharMacarrao()
        {
            Console.WriteLine("Cozinhando macarrão...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Macarrão já está cozido!");
            Console.WriteLine();
        }

static void RefogarMolho()
        {
            Console.WriteLine("Refogando molho...");
            Thread.Sleep(2000);
            Console.WriteLine("Molho já está refogado!");
            Console.WriteLine();
		}
void Main()
{

		Stopwatch stopWacth = new Stopwatch();
			stopWacth.Start();
	 		CozinharMacarrao();
	        RefogarMolho();
			stopWacth.Stop();
		Console.WriteLine($"Tempo decorrido : {stopWacth.ElapsedMilliseconds / 1000.0} segundos");

		stopWacth.Reset();
			
		stopWacth.Start();
            Parallel.Invoke(() => CozinharMacarrao(),
            () => RefogarMolho());
		stopWacth.Stop();
		Console.WriteLine($"Tempo decorrido : {stopWacth.ElapsedMilliseconds / 1000.0} segundos");
		
}

// Define other methods and classes here