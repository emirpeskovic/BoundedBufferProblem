var objects = new object?[3] { null, null, null };

Thread t1 = new(delegate ()
{
    while (true)
    {
        if (objects.All(o => o == null))
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null)
                {
                    objects[i] = new();
                    Console.WriteLine("[P] has produced object #" + i);
                }
            }
        }
        else
        {
            Console.WriteLine("[P] could not produce a new object!");
        }

        Thread.Sleep(100 | 15);
    }
});

Thread t2 = new(delegate ()
{
    while (true)
    {
        if (objects.All(o => o != null))
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    objects[i] = null;
                    Console.WriteLine("[C] has consumed object #" + i);
                }
            }
        }
        else
        {
            Console.WriteLine("[C] could not consume an object!");
        }


        Thread.Sleep(100 | 15);
    }
});

// Start threads
t1.Start();
t2.Start();

// Join threads
t1.Join();
t2.Join();