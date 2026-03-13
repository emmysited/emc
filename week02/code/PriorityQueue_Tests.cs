using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and dequeue them.
    // Items: ("low", 1), ("high", 10), ("medium", 5)
    // Expected Result: "high" is returned first (priority 10), then "medium" (priority 5), then "low" (priority 1)
    // Defect(s) Found:
    //   1. The Dequeue loop used "_queue.Count - 1" as the upper bound, skipping the last item.
    //      This caused the last-added item to never be considered for highest priority.
    //   2. The item was never actually removed from the queue after being dequeued (missing RemoveAt call).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("high", 10);
        priorityQueue.Enqueue("medium", 5);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue three items where two share the same highest priority.
    // Items: ("first", 5), ("second", 5), ("third", 1)
    // Expected Result: "first" is returned before "second" because FIFO applies when priorities are equal.
    // Defect(s) Found:
    //   1. The original loop used >= instead of >, which meant a later item with equal priority
    //      would overwrite the index — breaking FIFO and returning the LAST equal-priority item instead of the FIRST.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 1);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException is thrown with message "The queue is empty."
    // Defect(s) Found: None - empty queue exception was already implemented correctly.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue one item and dequeue it. Verify queue is empty afterward.
    // Items: ("only", 3)
    // Expected Result: "only" is returned, and a subsequent dequeue throws an exception.
    // Defect(s) Found:
    //   1. The item was never removed from the queue (missing RemoveAt), so the queue
    //      never became empty and the second dequeue did not throw an exception.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 3);

        Assert.AreEqual("only", priorityQueue.Dequeue());

        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown after queue is emptied.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}
