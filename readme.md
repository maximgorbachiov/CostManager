In Future:
    1) Make all the services totally async on create, update, delete operations. E.g. when we create new transaction with new category we should:
      1.1 - Create on api gate side guid for new category and add it to the new transaction.
      1.2 - Create two messages with new category and new transaction creations for CategoryService and TransactionService.


        