cmake_minimum_required(VERSION 3.5)

project(cpcx LANGUAGES CXX)

set(CMAKE_INCLUDE_CURRENT_DIR ON)

set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)

# C++11
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Find Qt
find_package(Qt5 COMPONENTS Core Widgets Gui Multimedia REQUIRED)

# Glob source files
file(GLOB MAIN_SRC "src/*.[hc]pp")

# Add all source files to main executable
add_executable(cpcx ${MAIN_SRC} res.qrc)

# Set install targets for 'make install'
install(TARGETS cpcx CONFIGURATIONS Release RUNTIME DESTINATION bin)

# Link Qt/KDE
target_link_libraries(cpcx PRIVATE
		Qt5::Core
		Qt5::Widgets
		Qt5::Gui
		Qt5::Multimedia)