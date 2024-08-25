git checkout main;
git branch | Where-Object { $_ -notmatch 'main\d+$' } | ForEach-Object { git branch -D $_.replace(' ', '') }