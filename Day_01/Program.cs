// See https://aka.ms/new-console-template for more information

using Day_01;

int MaxCalories()
{
    var maxSum = 0;
    var currentSum = 0;
    foreach (var row in Input.Data.Split(Environment.NewLine).Select(row => row.Trim()))
    {
        if (row == string.Empty)
        {
            // new group will start next, work with maxSum & erase current
            maxSum = Math.Max(maxSum, currentSum);
            currentSum = 0;
        }
        else
        {
            currentSum += Int32.Parse(row.Trim());    
        }
    }

    return maxSum;
}

int TopThreeCalories()
{
    var top1 = 0;
    var top2 = 0;
    var top3 = 0;
    var currentSum = 0;
    void SaveTop3(int currentSum)
    {
        if (currentSum > top1)
        {
            top3 = top2;
            top2 = top1;
            top1 = currentSum;
        } else if (currentSum > top2)
        {
            top3 = top2;
            top2 = currentSum;
        } else if (currentSum > top3)
        {
            top3 = currentSum;
        }
    }

    foreach (var row in Input.Data.Split(Environment.NewLine).Select(row => row.Trim()))
    {
        if (row == string.Empty)
        {
            // new group will start next, work with maxSum & erase current
            SaveTop3(currentSum);
            currentSum = 0;
        }
        else
        {
            currentSum += Int32.Parse(row.Trim());
        }
    }

    return top1 + top2 + top3;
}



Console.WriteLine($"Max: {MaxCalories()}");
Console.WriteLine($"Top Three: {TopThreeCalories()}");
