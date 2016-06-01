#!/bin/bash

if [ $# -ne 1 ]
then
    echo "Usage: $0 <filename>"
    echo "<filename> : the dictionary's filename"
    echo ""
    exit
else
    dico_file=$1
fi


if [ ! -e $dico_file ]
then
    echo "Error: the given dictionary file does not exist."
    exit
fi


#====================================
# statistics on letter occurrences
#====================================

echo "Display the number of occurrences of each letter"

# count the number of occurrences of each letter in the file (how many words contain that letter)
# - for		  => each letter (upper mode) in the alphabet (sequence A..Z)
# - grep	  => search for the given letter
# - wc -l	  => count the number of matching lines
# - printf "%7d"  => print the given numerical variable right aligned (width of 7)

for letter in {A..Z}
do
    letter_count=$(grep $letter $dico_file | wc -l)
    printf " $letter - %7d\n" $letter_count 
done

echo ""

