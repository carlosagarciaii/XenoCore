#!/bin/bash

# Print Message Functions


# --------------------------------------------------------
# -- HEADER FUNCTIONS
# --------------------------------------------------------


    createHeader(){
        local div=""
        local message="$1"

        message="-- $message"
        local length=${#message}
        for((i=0; i< length +4; i++)); do
            div="$div-"
        done

        echo ""
        echo -e "\e[93m"
        echo "$div"
        echo "$message"
        echo "$div"
        echo -e "\e[32m"
    }

    createPrompt(){
        local div=""
        local message="$1"


        message="++ $message"
        local length=${#message}
        for((i=0; i< length +4; i++)); do
            div="$div+"
        done

        echo ""
        echo -e "\e[92m"
        echo "$message"
        echo "$div"
        echo -e "\e[32m"

    }

    createSection(){

        local div=""
        local message="$1"

        message="-- $message"
        local length=${#message}
        for((i=0; i< length +4; i++)); do
            div="$div-"
        done

        echo ""
        echo -e "\e[94m"
        echo -e "  $message"
        echo "  $div"
        echo -e "\e[32m"
    }

    createError(){
        local div=""
        local message="$1"
        local errorCode="$2"

        message="-- Message:  $message"
        local length=${#message}
        for((i=0; i< length +4; i++)); do
            div="$div-"
        done

        echo ""
        echo -e "\e[91m"
        echo "$div"
        echo "-- ERROR"
        echo "$message"
        if [ ! -z "$errorCode" ]; then
            echo "-- Code:    $errorCode"
        fi
        echo "$div"
        echo -e "\e[32m"
    }

    createSuccess(){
        local div=""
        local message="$1"

        message="-- $message"
        local length=${#message}
        for((i=0; i< length +4; i++)); do
            div="$div-"
        done

        echo ""
        echo -e "\e[96m"
        echo -e "  $message  \e[55m"
        echo "  $div"
        echo -e "\e[32m"
    }

    checkError(){
        local outCode="$1"
        local failMessage="$2"
        local successMessage="$3"

        if [ $outCode -ne 0 ]; 
            then
                createError "$failMessage" "$outCode"
                exit 1
                return 1

            else
                if [[ ! -z "$successMessage" ]]; then
                    createSuccess "$successMessage"
                fi 
                return 0

        fi
    }

    createRunHelpText(){

        local div=""
        local message="$1"
        local exampleText="$2"
        local scriptName=$(basename "$0")
        local totalDivLength="--  $message  "
        local length=${#totalDivLength}
        for((i=0; i< length +4; i++)); do
            div="$div-"
        done

        echo ""
        echo -e "\e[96m"
        echo "$div"
        echo -e "-- \e[92mHELP DETAILS:"
        echo -e "\e[96m--    \e[93m$message"
        echo -e "\e[96m--    \e[94mIE: $scriptName $exampleText"
        echo -e "\e[96m$div"
        echo -e "\e[32m"
    }



# --------------------------------------------------------
# -- DOT NET VALIDATIONS
# --------------------------------------------------------

	lintDotNet(){
		createSection "Checking for dotnet-format"
		dotnet --version
		if [ $0 -ne 0 ];
			then
				createError "!!ERROR: dotnet is NOT INSTALLED" $0;
				exit 1;
				return 1;
		fi
		
		dotnet format --version
		if [ $0 -ne 0 ]; 
			then
				createSection "Installing dotnet-format";
				dotnet tool install -g dotnet-format;
			else
				echo -e "\e[34m  ## Confirmed dotnet-format is installed \e[32m";
		fi
	
		find ./ -name "*.csproj" -exec dotnet format {} \; ;
		
	}




