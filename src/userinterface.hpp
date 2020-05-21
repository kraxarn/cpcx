#pragma once

#include <QPixmap>

class UserInterface
{
public:
	virtual void setBackground(const QPixmap &background) = 0;
	virtual void setWindowVisible(bool visible) = 0;
	virtual void moveWindow(int x, int y) = 0;
};
