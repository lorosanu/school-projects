# bash_statistics-on-letters

This project contains a bash script which computes simple statistics on letters

## Description of the script
Output statistics on the letters present in a dictionary file

## Usage of the script
langstat.sh <file> --opt={alpha, num, freq, hist, wlength}

<file> : the dictionary's filename (mandatory)
--opt  : optional argument, the display mode.
         Available display modes :
	  - 'alpha'   = sort in alphabetic order of letters (* by default)
	  - 'num'     = sort in decreasing order the number of occurrences for each letter
	  - 'freq'    = sort in decreasing order the frequency of occurrences for each letter
	  - 'hist'    = draw a horizontal 'histogram' on the frequency of occurrences for each letter
	  - 'wlength' = statistics on the words length

