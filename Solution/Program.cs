// Storage of objects array
var objects = new object?[3] { null, null, null };

// Our 'lock'
var mutex = new Mutex();

// Thread that produces new objects
Thread t1 = new(delegate ()
{
    while (true)
    {
        mutex.WaitOne();

        try
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null)
                {
                    objects[i] = new object();
                    Console.WriteLine("-- Produced object #{0}", i);
                }
                else
                {
                    Console.WriteLine("Could not produce 3 objects!");
                }
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }

        Thread.Sleep(100 | 15);
    }
});

// Thread that consumes objects
Thread t2 = new(delegate ()
{
    while (true)
    {
        mutex.WaitOne();

        try
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    Console.WriteLine("--- Consumed object #{0}", i);
                    objects[i] = null;
                }
                else
                {
                    Console.WriteLine("Could not consume 3 objects!");
                }
            }
        }
        finally
        {
            mutex.ReleaseMutex();
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