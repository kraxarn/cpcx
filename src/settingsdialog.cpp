#include "settingsdialog.hpp"
SettingsDialog::SettingsDialog(QWidget *parent) : QDialog(parent)
{
	auto mainLayout = new QHBoxLayout();
	auto leftLayout = new QVBoxLayout();
	mainLayout->addLayout(leftLayout);

	auto general = new QGroupBox("General");
	auto generalLayout = new QVBoxLayout();
	generalLayout->addWidget(new QCheckBox("Lower other sounds"));
	generalLayout->addWidget(new QCheckBox("Ignore fullscreen"));
	leftLayout->addWidget(general, 0, Qt::AlignTop);
	general->setLayout(generalLayout);

	auto personality = new QGroupBox("Personality");
	auto personalityLayout = new QVBoxLayout();
	auto personalitySlider = new QSlider(Qt::Orientation::Horizontal);
	personalitySlider->setMinimum(0);
	personalitySlider->setMaximum(3);
	personalitySlider->setValue(3);
	personalityLayout->addWidget(personalitySlider);
	auto personalityLabel = new QLabel("Overkill");
	personalityLayout->addWidget(personalityLabel);
	personality->setLayout(personalityLayout);
	leftLayout->addWidget(personality, 0, Qt::AlignTop);

	auto rightLayout = new QVBoxLayout();
	mainLayout->addLayout(rightLayout);

	auto voice = new QGroupBox("Voice");
	auto voiceLayout = new QVBoxLayout();
	auto voiceBox = new QComboBox();
	voiceBox->addItem("Default");
	voiceLayout->addWidget(voiceBox);
	voice->setLayout(voiceLayout);
	rightLayout->addWidget(voice);

	auto notifications = new QGroupBox("Notifications");
	auto notificationsLayout = new QVBoxLayout();
	auto notificationsBox = new QComboBox();
	notificationsBox->addItem("All");
	notificationsLayout->addWidget(notificationsBox);
	notifications->setLayout(notificationsLayout);
	rightLayout->addWidget(notifications);

	auto complainAbout = new QGroupBox("Complain About");
	auto complainAboutLayout = new QVBoxLayout();
	auto complainAboutBox = new QComboBox();
	complainAboutBox->addItem("All");
	complainAboutLayout->addWidget(complainAboutBox);
	complainAbout->setLayout(complainAboutLayout);
	rightLayout->addWidget(complainAbout);

	auto saveButton = new QPushButton("Save");
	QAbstractButton::connect(saveButton, &QAbstractButton::clicked, [this](bool checked) {
		close();
	});
	rightLayout->addWidget(saveButton);

	setWindowTitle("cpcx settings");
	setLayout(mainLayout);
}