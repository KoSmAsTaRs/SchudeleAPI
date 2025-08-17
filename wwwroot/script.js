document.addEventListener('DOMContentLoaded', function() {
    const baseUrl = 'https://26.106.178.138:5001/api/Schedules'; // Замените на ваш URL API
    const formsContainer = document.getElementById('formsContainer');
    const dataDisplay = document.getElementById('dataDisplay');

    // Обработчики кнопок
    document.getElementById('getAllBtn').addEventListener('click', showGetAllForm);
    document.getElementById('getByIdBtn').addEventListener('click', showGetByIdForm);
    document.getElementById('getByGroupBtn').addEventListener('click', showGetByGroupForm);
    document.getElementById('getByTeacherBtn').addEventListener('click', showGetByTeacherForm);
    document.getElementById('createBtn').addEventListener('click', showCreateForm);
    document.getElementById('updateBtn').addEventListener('click', showUpdateForm);
    document.getElementById('deleteBtn').addEventListener('click', showDeleteForm);
    // Глобальные переменные для хранения данных
let teachers = [];
let subjects = [];
let groups = [];

// Функции для загрузки данных (добавить перед существующими функциями)
async function loadTeachers() {
    try {
        const response = await fetch('/api/Teachers/GetTeachers');
        teachers = await response.json();
    } catch (error) {
        console.error('Ошибка загрузки преподавателей:', error);
    }
}

async function loadSubjects() {
    try {
        const response = await fetch('/api/Subjects/GetSubjects');
        subjects = await response.json();
    } catch (error) {
        console.error('Ошибка загрузки предметов:', error);
    }
}

async function loadGroups() {
    try {
        const response = await fetch('/api/Groups/GetGroups');
        groups = await response.json();
    } catch (error) {
        console.error('Ошибка загрузки групп:', error);
    }
}

    // Функции для отображения форм
    function showGetAllForm() {
        clearForms();
        
        const form = document.createElement('div');
        form.innerHTML = `
            <h3>Получить все расписания</h3>
            <button id="submitGetAll">Запросить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitGetAll').addEventListener('click', getAllSchedules);
    }

    function showGetByIdForm() {
        clearForms();
        
        const form = document.createElement('form');
        form.id = 'getByIdForm';
        form.innerHTML = `
            <h3>Получить расписание по ID</h3>
            <div>
                <label for="scheduleId">ID расписания:</label>
                <input type="number" id="scheduleId" required>
            </div>
            <button type="button" id="submitGetById">Запросить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitGetById').addEventListener('click', getScheduleById);
    }

    function showGetByGroupForm() {
        clearForms();
        
        const form = document.createElement('form');
        form.id = 'getByGroupForm';
        form.innerHTML = `
            <h3>Получить расписание по группе</h3>
            <div>
                <label for="groupId">ID группы:</label>
                <input type="number" id="groupId" required>
            </div>
            <button type="button" id="submitGetByGroup">Запросить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitGetByGroup').addEventListener('click', getSchedulesByGroup);
    }

    function showGetByTeacherForm() {
        clearForms();
        
        const form = document.createElement('form');
        form.id = 'getByTeacherForm';
        form.innerHTML = `
            <h3>Получить расписание по преподавателю</h3>
            <div>
                <label for="teacherId">ID преподавателя:</label>
                <input type="number" id="teacherId" required>
            </div>
            <button type="button" id="submitGetByTeacher">Запросить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitGetByTeacher').addEventListener('click', getSchedulesByTeacher);
    }

    function showCreateForm() {
    clearForms();
    
    const form = document.createElement('form');
    form.id = 'createForm';
    form.innerHTML = `
        <h3>Создать новое расписание</h3>
        <div class="form-group">
            <label for="teacherSelect">Преподаватель:</label>
            <select id="teacherSelect" class="form-control" required>
                <option value="">Выберите преподавателя</option>
                ${teachers.map(t => `<option value="${t.id}">${t.name}</option>`).join('')}
            </select>
        </div>
        <div class="form-group">
            <label for="subjectSelect">Предмет:</label>
            <select id="subjectSelect" class="form-control" required>
                <option value="">Выберите предмет</option>
                ${subjects.map(s => `<option value="${s.id}">${s.name}</option>`).join('')}
            </select>
        </div>
        <div class="form-group">
            <label for="groupSelect">Группа:</label>
            <select id="groupSelect" class="form-control" required>
                <option value="">Выберите группу</option>
                ${groups.map(g => `<option value="${g.id}">${g.name}</option>`).join('')}
            </select>
        </div>
        <div class="form-group">
            <label for="roomCreate">Аудитория:</label>
            <input type="text" id="roomCreate" class="form-control" required>
        </div>
        <!-- Остальные поля формы остаются без изменений -->
        <button type="button" id="submitCreate" class="btn btn-primary">Создать</button>
    `;
    formsContainer.appendChild(form);
    
    document.getElementById('submitCreate').addEventListener('click', createSchedule);
}

    function showUpdateForm() {
        clearForms();
        
        const form = document.createElement('form');
        form.id = 'updateForm';
        form.innerHTML = `
            <h3>Обновить расписание</h3>
            <div>
                <label for="scheduleIdUpdate">ID расписания:</label>
                <input type="number" id="scheduleIdUpdate" required>
            </div>
            <div>
                <label for="teacherIdUpdate">ID преподавателя (необязательно):</label>
                <input type="number" id="teacherIdUpdate">
            </div>
            <div>
                <label for="subjectIdUpdate">ID предмета (необязательно):</label>
                <input type="number" id="subjectIdUpdate">
            </div>
            <div>
                <label for="groupIdUpdate">ID группы (необязательно):</label>
                <input type="number" id="groupIdUpdate">
            </div>
            <div>
                <label for="roomUpdate">Аудитория (необязательно):</label>
                <input type="text" id="roomUpdate">
            </div>
            <div>
                <label for="dayOfWeekUpdate">День недели (необязательно):</label>
                <select id="dayOfWeekUpdate">
                    <option value="">Не изменять</option>
                    <option value="1">Понедельник</option>
                    <option value="2">Вторник</option>
                    <option value="3">Среда</option>
                    <option value="4">Четверг</option>
                    <option value="5">Пятница</option>
                    <option value="6">Суббота</option>
                    <option value="7">Воскресенье</option>
                </select>
            </div>
            <div>
                <label for="startTimeUpdate">Время начала (необязательно):</label>
                <input type="time" id="startTimeUpdate">
            </div>
            <div>
                <label for="endTimeUpdate">Время окончания (необязательно):</label>
                <input type="time" id="endTimeUpdate">
            </div>
            <div>
                <label for="weekTypeUpdate">Тип недели (необязательно):</label>
                <select id="weekTypeUpdate">
                    <option value="">Не изменять</option>
                    <option value="0">Каждую неделю</option>
                    <option value="1">Числитель</option>
                    <option value="2">Знаменатель</option>
                </select>
            </div>
            <button type="button" id="submitUpdate">Обновить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitUpdate').addEventListener('click', updateSchedule);
    }

    function showDeleteForm() {
        clearForms();
        
        const form = document.createElement('form');
        form.id = 'deleteForm';
        form.innerHTML = `
            <h3>Удалить расписание</h3>
            <div>
                <label for="scheduleIdDelete">ID расписания:</label>
                <input type="number" id="scheduleIdDelete" required>
            </div>
            <button type="button" id="submitDelete">Удалить</button>
        `;
        formsContainer.appendChild(form);
        
        document.getElementById('submitDelete').addEventListener('click', deleteSchedule);
    }

    function clearForms() {
        formsContainer.innerHTML = '';
    }

    // Функции для работы с API
    async function getAllSchedules() {
        try {
            const response = await fetch(`${baseUrl}/GetAllSchedules`);
            if (!response.ok) {
                throw new Error('Ошибка при получении данных');
            }
            const data = await response.json();
            displayData(data);
        } catch (error) {
            showError(error.message);
        }
    }

    async function getScheduleById() {
        const id = document.getElementById('scheduleId').value;
        if (!id) {
            showError('Введите ID расписания');
            return;
        }

        try {
            const response = await fetch(`${baseUrl}/GetScheduleById/${id}`);
            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Расписание не найдено');
                } else {
                    throw new Error('Ошибка при получении данных');
                }
            }
            const data = await response.json();
            displayData([data]); // Обернем в массив для унификации отображения
        } catch (error) {
            showError(error.message);
        }
    }

    async function getSchedulesByGroup() {
        const groupId = document.getElementById('groupId').value;
        if (!groupId) {
            showError('Введите ID группы');
            return;
        }

        try {
            const response = await fetch(`${baseUrl}/GetSchedulesByGroup/${groupId}`);
            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Расписание для группы не найдено');
                } else {
                    throw new Error('Ошибка при получении данных');
                }
            }
            const data = await response.json();
            displayData([data]); // Обернем в массив для унификации отображения
        } catch (error) {
            showError(error.message);
        }
    }

    async function getSchedulesByTeacher() {
        const teacherId = document.getElementById('teacherId').value;
        if (!teacherId) {
            showError('Введите ID преподавателя');
            return;
        }

        try {
            const response = await fetch(`${baseUrl}/GetScheduleByTeacherId?teacher_id=${teacherId}`);
            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Расписание для преподавателя не найдено');
                } else {
                    throw new Error('Ошибка при получении данных');
                }
            }
            const data = await response.json();
            displayData(Array.isArray(data) ? data : [data]);
        } catch (error) {
            showError(error.message);
        }
    }

    async function createSchedule() {
        const formData = {
            teacher_id: parseInt(document.getElementById('teacherIdCreate').value),
            subject_id: parseInt(document.getElementById('subjectIdCreate').value),
            group_id: parseInt(document.getElementById('groupIdCreate').value),
            room: document.getElementById('roomCreate').value,
            day_of_week: parseInt(document.getElementById('dayOfWeekCreate').value),
            start_time: document.getElementById('startTimeCreate').value + ':00', // Добавляем секунды
            end_time: document.getElementById('endTimeCreate').value + ':00',
            week_type: parseInt(document.getElementById('weekTypeCreate').value)
        };

        try {
            const response = await fetch(`${baseUrl}/CreateSchedule`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка при создании расписания');
            }

            const data = await response.json();
            showSuccess('Расписание успешно создано!');
            displayData([data]);
        } catch (error) {
            showError(error.message);
        }
    }

    async function updateSchedule() {
        const id = document.getElementById('scheduleIdUpdate').value;
        if (!id) {
            showError('Введите ID расписания');
            return;
        }

        const formData = {};
        // Добавляем только те поля, которые были заполнены
        const teacherId = document.getElementById('teacherIdUpdate').value;
        if (teacherId) formData.teacher_id = parseInt(teacherId);
        
        const subjectId = document.getElementById('subjectIdUpdate').value;
        if (subjectId) formData.subject_id = parseInt(subjectId);
        
        const groupId = document.getElementById('groupIdUpdate').value;
        if (groupId) formData.group_id = parseInt(groupId);
        
        const room = document.getElementById('roomUpdate').value;
        if (room) formData.room = room;
        
        const dayOfWeek = document.getElementById('dayOfWeekUpdate').value;
        if (dayOfWeek) formData.day_of_week = parseInt(dayOfWeek);
        
        const startTime = document.getElementById('startTimeUpdate').value;
        if (startTime) formData.start_time = startTime + ':00';
        
        const endTime = document.getElementById('endTimeUpdate').value;
        if (endTime) formData.end_time = endTime + ':00';
        
        const weekType = document.getElementById('weekTypeUpdate').value;
        if (weekType) formData.week_type = parseInt(weekType);

        try {
            const response = await fetch(`${baseUrl}/UpdateSchedule/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Расписание не найдено');
                } else {
                    const errorData = await response.json();
                    throw new Error(errorData.message || 'Ошибка при обновлении расписания');
                }
            }

            showSuccess('Расписание успешно обновлено!');
            // Получаем обновленные данные для отображения
            getScheduleById(id);
        } catch (error) {
            showError(error.message);
        }
    }

    async function deleteSchedule() {
        const id = document.getElementById('scheduleIdDelete').value;
        if (!id) {
            showError('Введите ID расписания');
            return;
        }

        try {
            const response = await fetch(`${baseUrl}/DeleteSchedule/${id}`, {
                method: 'DELETE'
            });

            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Расписание не найдено');
                } else {
                    throw new Error('Ошибка при удалении расписания');
                }
            }

            showSuccess('Расписание успешно удалено!');
            dataDisplay.innerHTML = ''; // Очищаем отображение данных
        } catch (error) {
            showError(error.message);
        }
    }

    // Функции для отображения данных и сообщений
    function displayData(schedules) {
        if (!schedules || schedules.length === 0) {
            dataDisplay.innerHTML = '<p>Данные не найдены</p>';
            return;
        }

        const table = document.createElement('table');
        table.innerHTML = `
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Преподаватель</th>
                    <th>Предмет</th>
                    <th>Группа</th>
                    <th>Аудитория</th>
                    <th>День недели</th>
                    <th>Время начала</th>
                    <th>Время окончания</th>
                    <th>Тип недели</th>
                </tr>
            </thead>
            <tbody>
                ${schedules.map(schedule => `
                    <tr>
                        <td>${schedule.id || '-'}</td>
                        <td>${schedule.teacherName || '-'}</td>
                        <td>${schedule.subjectName || '-'}</td>
                        <td>${schedule.groupName || '-'}</td>
                        <td>${schedule.room || '-'}</td>
                        <td>${getDayOfWeekName(schedule.day_of_week)}</td>
                        <td>${schedule.start_time || '-'}</td>
                        <td>${schedule.end_time || '-'}</td>
                        <td>${getWeekTypeName(schedule.week_type)}</td>
                    </tr>
                `).join('')}
            </tbody>
        `;
        
        dataDisplay.innerHTML = '';
        dataDisplay.appendChild(table);
    }

    function getDayOfWeekName(dayNumber) {
        const days = {
            1: 'Понедельник',
            2: 'Вторник',
            3: 'Среда',
            4: 'Четверг',
            5: 'Пятница',
            6: 'Суббота',
            7: 'Воскресенье'
        };
        return days[dayNumber] || '-';
    }

    function getWeekTypeName(weekType) {
        const types = {
            0: 'Каждую неделю',
            1: 'Числитель',
            2: 'Знаменатель'
        };
        return types[weekType] || '-';
    }

    function showError(message) {
        const errorDiv = document.createElement('div');
        errorDiv.className = 'error';
        errorDiv.textContent = message;
        errorDiv.style.color = 'red';
        errorDiv.style.margin = '10px 0';
        
        dataDisplay.innerHTML = '';
        dataDisplay.appendChild(errorDiv);
    }

    function showSuccess(message) {
        const successDiv = document.createElement('div');
        successDiv.className = 'success';
        successDiv.textContent = message;
        successDiv.style.color = 'green';
        successDiv.style.margin = '10px 0';
        
        formsContainer.appendChild(successDiv);
        
        // Удаляем сообщение через 3 секунды
        setTimeout(() => {
            successDiv.remove();
        }, 3000);
    }
});