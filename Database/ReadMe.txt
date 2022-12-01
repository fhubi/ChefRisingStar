To create a SQLite database start by downloading SQLite:
https://sqlite.org/download.html

Copy the dlls to any folder you would like to use SQLite in.
To create a Database in the directory where the SQLite3.exe is.

Open a command line to directory
enter: sqlite3 ltdc.db

To seed the DB from the create script:
At the command prompt enter:
sqlite> .read DatabaseSchema.sql

This should create the tables and some basic data.

To check which tables were created use this command:
sqlite> .tables

ex. output:
AchievementSteps      SocialMediaSites      UserMetrics
Achievements          Teams                 UserRecipes
FavoriteTypes         UserAchievementSteps  UserSocialMediaSites
Metrics               UserAchievements      Users
Schools               UserFavortites

To exit use:
sqlite> .exit

To connect to the DB you will need the code specific dependency library written for for that language.
Available for downlaod from link above.

