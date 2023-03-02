import { defineStore } from "pinia";

export const useProfileStore = defineStore("profile", {
    state: () => {
        return {
            name: "",
            role: ""
        };
    },

    getters: {
        getName(state) {
            return state.name;
        },

        getRole(state) {
            return state.role;
        }
    },

    actions: {
        //
    }
});
