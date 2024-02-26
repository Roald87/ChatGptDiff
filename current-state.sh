#!/bin/bash

# Define the directory to search in
search_dir="."

# Find files with specific extensions recursively and loop through them
find "$search_dir" -type f \( -name "*.cs" -o -name "*.razor" \) | while read -r filename; do
    echo Filename: "$filename" # Print the filename
    echo File content: # Print a newline for better readability
    cat "$filename" # Print the content of the file
    echo # Print a newline to separate the contents of different files
done
