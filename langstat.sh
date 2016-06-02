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
        echo -e "Display the number of occurrences of each letter\n"

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
        tmp_file="tmp_stats.txt"
	printf '' > $tmp_file

	echo -e "Display the number of occurrences for each letter\n(decreasing order of occurrences)\n"

	# count the number of occurrences of each letter in the file (how many words contain that letter)
	#  - for          -> each letter (upper mode) in the alphabet (sequence A..Z)
	#  - grep         -> search for the given letter
	#  - wc -l        -> count the number of matching lines
	#  - printf "%8d" -> print the given numeric variable right aligned (width of 8)"

	for letter in {A..Z}
	do
	    letter_count=$(grep $letter $dico_file | wc -l)
	    printf "%8d - $letter\n" $letter_count >> $tmp_file
	done

	# sort by the first field (first column) : the number of occurrences (numerical value);
	# display reversed : decreasing order

	sort -k1 -n -r $tmp_file
	rm -f $tmp_file

	;;
    "freq")
        tmp_file="tmp_stats.txt"
	printf '' > $tmp_file

	echo -e "Display the frequency of occurrences for each letter\n(decreasing order of occurrences)\n"

	# count the number of occurrences of each letter in the file (how many words contain that letter)
	# divide the number of occurrences by the total number of words in the file
	#  - for          -> each letter (upper mode) in the alphabet (sequence A..Z)
	#  - grep         -> search for the given letter
	#  - wc -l        -> count the number of matching lines
	#  - printf "%6s" -> print the given variable right aligned (width of 6)"

	nwords=$(cat $dico_file | wc -l)
	for letter in {A..Z}
	do
	    letter_count=$(grep $letter $dico_file | wc -l)
	    letter_freq=$(echo $letter_count $nwords | awk '{printf "%.2f", $1 * 100 / $2}')
	    printf "%6s%% - $letter\n" $letter_freq >> $tmp_file
	done

	# sort by the first field (first column) : the frequency of occurrences (numerical value);
        # display reversed : decreasing order

	sort -k1 -n -r $tmp_file
	rm -f $tmp_file

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

