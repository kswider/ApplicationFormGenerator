# ApplicationFormGenerator

This project was created to easily generate fake application form files for Machine Learning purposes.

It can be easily used for creating any amount of docs using provided schema. Example of schemas can be found in "./Data/Any" and "./Data/ECTSDeficit" directories.

# Creating schema

## Tokens
There are 7 types of tokens for now:
			
NAME   

SURNAME

NUMBERS_X

NUMBER_RANGE_X_Y

DATE

DEGREE

COURSE

To use token in you schema you need to apply double underscore (__) before and after token.

Example of generated values:

Name: \_\_NAME\_\_ => Mirosław

Surname: \_\_SURNAME\_\_ => Lis

Sequence of numbers: \_\_NUMBERS_10\_\_ => 0697459296

Number from given range (upper bound exclusive): \_\_NUMBER_RANGE_100_1000\_\_ => 321

Date (from last 30 days): \_\_DATE\_\_ => 26.05.2020

Degree: \_\_DEGREE\_\_ => Materiały i Technologie Metali Nieżelaznych

Course: \_\_COURSE\_\_ => Mikro i nanotechnologie w biofizyce


Additionally there are two type of token modifiers:

## Const modifier
This modifier should be used for reusing generated token multiple times in on schema. It can be applied by preceeding the token with 'C_'.

Example:

Generating exactly same name twice: 

\_\_C_NAME\_\_ \_\_C_NAME\_\_ => Stanisław Stanisław

Note:

If you need to have more than one const token inside your schema, you can add number after your token:

Example:

Generating different than previous name, exactly twice: 

\_\_C_NAME_2\_\_ \_\_C_NAME_2\_\_ => Ewelina Ewelina


## Decisive modifier
This modifier should be used for creating schemas for MachineLearning needs. It can be applied by merging two tokens with slash sign (/). 
The token before slash will be used in positive scenarios. The token after slash will be used in negative scenarios.
Type of scenarios is chosen after start of the generator.

Example:

In positive scenario number from range [1;5) will be generated. In negative scenario number from range [5;100) will be generated.

(positive case) \_\_NUMBER_RANGE_1_5/NUMBER_RANGE_5_100\_\_ => 3 

(negative case) \_\_NUMBER_RANGE_1_5/NUMBER_RANGE_5_100\_\_ => 63


# Running app
To run the application you need to clone the repo and have .net core 3.1 installed on your machine.

After that open your favourite console, go to the project directory and run "dotnet run" command.

# Using your own schemas
If you want to use your own schemas, place them in folder "./Data/Any", run the generator and select "Any document" for the document type. Generator will randomly select schema from all schemas in this folder. 
