UML
----------
BankAccount class
----------
-customerName : string
-accountID : int
-balance : float
-transactions : List
----------
+BankAccount() //constructor
+BankAccount(string,float) //constructor given cuistomer name and initital balance
+accountID(int) //properties
+balance(float)
+transactions(List)
+Deposit(float) //deposit method
+Withdrawal(float) //withdrawal method



TestCases
----------
CREATE ACCOUNT
1. Create account with valid customer name and initial balance

    Expect correct properties set (AccountId not empty, name matches, balance equals initial, transactions empty).

2. Create account with negative initial balance

    Expect an error/exception.

3. Create account with empty or whitespace customer name

    Expect an error/exception.

DEPOSIT

4. Deposit a positive amount

    Balance increases by deposit amount.

    Transaction added with type Deposit.

    Transaction balanceAfter matches new balance.

5. Deposit zero amount

    Expect an error/exception.

6. Deposit negative amount

    Expect an error/exception.

WITHDRAW

7. Withdraw a valid amount within balance

    Balance decreases by withdrawal amount.

    Transaction added with type Withdrawal.

    Transaction balanceAfter matches new balance.

8. Withdraw zero amount

    Expect an error/exception.

9. Withdraw negative amount

    Expect an error/exception.

10. Withdraw more than available balance (overdraft)

    Expect an error/exception.

TRANSACTION HISTORY

11. Multiple operations (deposit then withdraw)

    Transactions list keeps correct order.

    Each transaction’s balanceAfter reflects the running total.

12. Transaction note stored correctly

    Notes provided in deposit/withdraw methods appear in transaction history.
