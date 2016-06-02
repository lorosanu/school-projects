#!/bin/bash

#======================================
# process the command line arguments
#======================================

if [ $# -ne 1  ] && [ $# -ne 2 ]
then
    echo "Description : output statistics on the letters present in a dictionary file"
    echo ""
    echo "Usage: $0 <file> --opt={alpha, num, freq, hist, wlength}"
    echo "<file> : the dictionary's filename"
    echo "--opt  : optional argument, the display mode."
    echo "         Choose a value from : "
    echo "          - 'alpha'   = sort in alphabetic order of letters (* by default)"
    echo "          - 'num'     = sort in decreasing order the number of occurrences for each letter"
    echo "          - 'freq'    = sort in decreasing order the frequency of occurrences for each letter"
    echo "          - 'hist'    = draw a horizontal 'histogram' on the frequency of occurrences for each letter"
    echo "          - 'wlength' = statistics on the words length"
    echo ""
    exit
fi

if [ $# -ge 1 ]
then
    dico_file=$1

    if [ ! -e $dico_file ]
    then
        echo "Error: the given dictionary file does not exist."
	exit
    fi

    display_opt="alpha"		# default display option

    if [ $# -eq 2 ]
    then
        arg=$2

	if [[ $arg == --opt=* ]] && ( [[ $arg == *alpha ]] || [[ $arg == *num ]] || [[ $arg == *freq ]] || [[ $arg == *hist ]] || [[ $arg == *wlength ]] )
	then
	    display_opt=$(echo $arg| cut -d'=' -f 2)
	else
	    echo "Error: invalid second argument."
	    echo "Choose one from the list: --opt={alpha, num, freq, hist, wlength"
	    exit
	fi
    fi
fi


#======================================
# statistics on letter occurrences
#======================================

case $display_opt in
    "alpha")
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

	;;
    "num")
	;;
    "freq")
        ;;
    "hist")
    	;;
    "wlength")
    	;;
    *)
        echo "Error: unknown display option".
	echo "Choose one from the list: --opt={alpha, num, freq, hist, wlength"
	exit
	;;
esac 

echo ""

