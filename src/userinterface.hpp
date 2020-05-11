#pragma once

#include <QPixmap>

class UserInterface
{
public:
	virtual void setBackground(const QPixmap &background) = 0;
	virtual bool setVisible(bool visible) = 0;
	virtual void moveWindow(int x, int y) = 0;
};
