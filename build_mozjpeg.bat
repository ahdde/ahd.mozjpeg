cd mozjpeg
git reset --hard
git clean -f -d
cd ..
git apply jcparam.patch

mkdir mozjpeg\bin\win32
mkdir mozjpeg\bin\win64

cd mozjpeg\bin\win32
cmake -G "Visual Studio 15 2017" ..\..
msbuild /P:Configuration=MinSizeRel ALL_BUILD.vcxproj
cd ..\..\..

cd mozjpeg\bin\win64
cmake -G "Visual Studio 15 2017 Win64" ..\..
msbuild /P:Configuration=MinSizeRel ALL_BUILD.vcxproj
cd ..\..\..

copy /Y mozjpeg\bin\win32\MinSizeRel\turbojpeg.dll LibJpegWrapper\costura32\turbojpeg.dll
copy /Y mozjpeg\bin\win64\MinSizeRel\turbojpeg.dll LibJpegWrapper\costura64\turbojpeg.dll