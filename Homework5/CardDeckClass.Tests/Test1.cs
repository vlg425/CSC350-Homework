
[TestClass]
public class BasicCardDeckPlayerTests
{
    [TestMethod]
    public void TestCardConstructor()//new card should have correct rank/suit and be face down
    {
        var c = new Card(Rank.Ace, Suit.Spades);
        Assert.AreEqual(Rank.Ace, c.Rank, "Rank not set.");
        Assert.AreEqual(Suit.Spades, c.Suit, "Suit not set.");
        Assert.IsFalse(c.FaceUp, "New card should start face down.");
    }

    [TestMethod]
    public void TestDeckConstructor()//fresh deck should have 52 unique cards and not be empty
    {
        var deck = new Deck();
        Assert.IsFalse(deck.Empty, "Fresh deck should not be empty.");
        Assert.AreEqual(52, deck.Cards.Count, "Fresh deck should have 52 cards.");

        var uniq = new HashSet<string>(deck.Cards.Select(c => $"{c.Rank}-{c.Suit}"));
        Assert.AreEqual(52, uniq.Count, "Deck should contain 52 unique rank/suit combos.");
    }

    [TestMethod]
    public void TestTakeTopCard() //deck size should drop by 1 after taking top card
    {
        var deck = new Deck();
        int before = deck.Cards.Count;

        var top = deck.TakeTopCard();

        Assert.IsNotNull(top, "Should return a card from non-empty deck.");
        Assert.AreEqual(before - 1, deck.Cards.Count, "Deck size should drop by 1 after TakeTopCard.");
    }

    [TestMethod]
    public void TestShuffle() //card order should be different after shuffle
    {
        var d = new Deck();
        var before = d.Cards.Select(c => $"{c.Rank}-{c.Suit}").ToList();

        d.Shuffle();
        var after = d.Cards.Select(c => $"{c.Rank}-{c.Suit}").ToList();

        // Not a proof, but catches a no-op Shuffle.
        bool changed = !before.SequenceEqual(after);
        Assert.IsTrue(changed, "Shuffle should change order in typical cases.");
    }

    [TestMethod]
    public void TestDrawCards() //should draw until emtpy
    {
        var deck = new Deck();
        var p = new Player("P");

        p.DrawFrom(deck, 60); // ask for more than 52
        Assert.AreEqual(52, p.CardCount, "Player should draw all available cards (52).");
        Assert.IsTrue(deck.Empty, "Deck should be empty after drawing all cards.");
    }
}
