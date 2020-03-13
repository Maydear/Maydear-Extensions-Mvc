# bin/sh

if [ $# -lt 1 ]; then
    echo "must input api-key"
    exit 2
fi

if ls ./publish/*nupkg >/dev/null 2>&1;then
    for i in `ls ./publish/*nupkg`
    do
    	echo "run nuget push $i -k $1";
        dotnet nuget push "$i" -k $1 -s https://www.nuget.org/api/v2/package
        echo "delete file $i";
        rm -rf $i;
    done
fi