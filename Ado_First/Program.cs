using Ado_First.Classes;
ConsoleService console = new();
bool ok = true;
console.Third();
while (ok)
{
    console.Forth();
    console.Third();
    ok = console.Again();
}
Console.ReadKey();