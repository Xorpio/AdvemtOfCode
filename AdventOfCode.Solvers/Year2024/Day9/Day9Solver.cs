
namespace AdventOfCode.Solvers.Year2024.Day9;

public class Day9Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        bool isFile = true;
        var id = 0;
        var list = new List<(int? id, int length)>();
        foreach (int n in puzzle[0].Select(c => int.Parse(c.ToString())))
        {
            if (isFile)
            {
                list.Add((id, n));

                id++;
            }
            else
            {
                list.Add((null, n));
            }

            isFile = !isFile;
        }

        SolvePart1(list.ToList());
        SolvePart2(list.ToList());

    }
    public void SolvePart2(List<(int? id, int lenght)> list)
    {
        var id = list.Last(i => i.id != null).id;

        while (id > 0)
        {
            var itemToMove = list.First(i => i.id == id);
            var indexOfItemToMove = list.IndexOf(itemToMove);
            for (int i = 0; i < list.Count && i < indexOfItemToMove; i++)
            {
                if (list[i].id != null)
                    continue;

                if (list[i].lenght == itemToMove.lenght)
                {
                    list[indexOfItemToMove] = (null, itemToMove.lenght);
                    list[i] = itemToMove;
                    break;
                }
                else if (list[i].lenght > itemToMove.lenght)
                {
                    list[indexOfItemToMove] = (null, itemToMove.lenght);
                    var left = list[i].lenght - itemToMove.lenght;
                    list[i] = itemToMove;
                    list.Insert(i + 1, (null, left));
                    break;
                }
            }
            id--;
        }

        var total = CalcTotal(list);
        GiveAnswer2(total);
    }

    public void SolvePart1(List<(int? id, int lenght)> list)
    {
        int index = 0;
        while (index < list.Count())
        {
            if (list[index].id == null)
            {
                var left = list[index].lenght;
                while (left > 0)
                {
                    var item = list.Last(i => i.id != null);
                    var indexOfItem = list.LastIndexOf(item);
                    if (indexOfItem < index)
                    {
                        break;
                    }

                    if (item.lenght == list[index].lenght)
                    {
                        list[index] = (item.id, left);
                        list.RemoveAt(indexOfItem);
                        left = 0;
                    }
                    else if (item.lenght < left)
                    {
                        left -= list[indexOfItem].lenght;
                        list[index] = item;
                        list.RemoveAt(indexOfItem);
                        list.Insert(index + 1, (null, left));
                        index++;
                    }
                    else
                    {
                        list[indexOfItem] = (item.id, item.lenght - left);
                        list[index] = (item.id, left);
                        left = 0;
                    }
                }
            }

            index++;
        }

        var total = CalcTotal(list);

        GiveAnswer1(total);
    }

    private decimal CalcTotal(List<(int? id, int lenght)> list)
    {
        decimal total = 0;
        var pos = 0;
        foreach (var item in list)
        {
            for (int i = 0; i < item.lenght; i++)
            {
                total += pos * item.id ?? 0;
                pos++;
            }
        }

        return total;
    }
}
