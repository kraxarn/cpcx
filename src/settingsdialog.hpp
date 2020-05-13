#pragma once

#include <QDialog>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QGroupBox>
#include <QCheckBox>
#include <QSlider>
#include <QLabel>
#include <QComboBox>
#include <QPushButton>

class SettingsDialog : public QDialog
{
public:
	SettingsDialog(QWidget *parent = nullptr);
};