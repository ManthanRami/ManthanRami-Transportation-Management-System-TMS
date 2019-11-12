# TMS-404

## Commit Information
- Please **never commit straight to master OR staging**.

- When you want to make changes, please **create your own branch** for the feature you are adding/changing. You may commit to this branch and this branch alone.

- To create a branch, go to the main repo page and look for the dropdown menu that says `Branch: Master`. The dropdown has a textbox where you can enter the name for your new branch and create it. Please make sure to base the new branch on `staging`.

- Once you're ready for your branch to be merged with `staging`, let the merge master know and your work will be merged into staging.

- Once code in staging is known to be stable and conflict-free, it will be merged with master.

- Commit small bits of code frequently. This way if we have to undo a commit, we're not undoing as much work and it keeps it more manageable.


### To clone this repository
1. Open a command line window at the location you wish to clone to
2. Clone the repository with `git clone https://github.com/sniddunc/TMS-404`
3. Switch to your development branch with `git checkout <branch>`. **Never modify master or staging**
